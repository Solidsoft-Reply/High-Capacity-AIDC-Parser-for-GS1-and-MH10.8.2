// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BarcodeParser.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2023 Solidsoft Reply Ltd. All rights reserved.
// </copyright>
// <license>
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </license>
// <summary>
// Barcode parser for Automatic Identification and Data Capture (AIDC) barcodes,
// including barcodes that conform to ISO/IEC 15434.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

[assembly: CLSCompliant(true)]
namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Properties;
using BarcodeScanner.Symbology;
using Syntax;
using Syntax.IsoIec15434DataEntities;
using System.Collections.Generic;
using Common;

/// <summary>
///   Delegate for pre-processor functions.
/// </summary>
/// <param name="input">The data input to be pre-processed.</param>
/// <param name="exceptions">Collection of exceptions.</param>
/// <returns>The pre-processed data.</returns>
public delegate string? Preprocessor(string? input, out IList<PreprocessorException>? exceptions);

/// <summary>
///   Barcode parser for Automatic Identification and Data Capture (AIDC) barcodes,
///   including barcodes that conform to ISO/IEC 15434.
/// </summary>
public static partial class Parser
{
    /// <summary>
    ///   Code generator for regular expression that matches the format identifier and preamble for binary data.
    /// </summary>
    [GeneratedRegex(@"09\u001d(?<fileName>[\w\s]{0,30})\u001d(?<compressionTechnique>[\w\s]{0,30})\u001d(?<numberOfBytes>0|\d{1,15})\u001d.*", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex MatchFormatIdentifierAndPreambleRegex();

    /// <summary>
    ///   Regular expression that matches the format identifier and preamble for binary data.
    /// </summary>
    private static readonly Regex RegexBinary = MatchFormatIdentifierAndPreambleRegex();

    /// <summary>
    ///   Parse the raw barcode data.
    /// </summary>
    /// <param name="data">The raw barcode data.</param>
    /// <param name="preProcessedData">The pre-processed data.</param>
    /// <param name="preProcessors">The pre-processor functions, provided as a delegate.</param>
    /// <returns>A pack identifier.</returns>
    [SuppressMessage("ReSharper", "CommentTypo")]
    public static IBarcode Parse(string data, out string preProcessedData, Preprocessor? preProcessors = null)
    {
        preProcessedData = data;

        // Is any data present?
        if (string.IsNullOrWhiteSpace(data))
        {
            var noDataBarcode = new Barcode(BarcodeType.NoIdentifier);
            noDataBarcode.AddException(
                new BarcodeException(1001, Resources.Barcodes_Error_001, true),
                ParseStatus.Invalid);
            return noDataBarcode;
        }

        var preprocessorExceptions = new List<PreprocessorException>();

        // Aggregate the results of each pre-processor.
        var input = preProcessors?.GetInvocationList().Aggregate(
            data,
            (current, preProcessor) =>
            {
                var processedData = ((Preprocessor)preProcessor).Invoke(current, out var exceptions)?.ToString() ?? string.Empty;
                preprocessorExceptions.AddRange(exceptions ?? new List<PreprocessorException>());
                return processedData;
            });

        // Determine the symbology and create the barcode record.
        var aimId = new AimDetector().Detect(input ?? string.Empty);
        var barcodeExceptions = preprocessorExceptions.ConvertToBarcodeExceptions();
        var barcode = new Barcode(aimId.BarcodeType, aimId.Modifier, barcodeExceptions);

        // Discard any AIM identifier.
        input = aimId.BarcodeData;

        if (string.IsNullOrWhiteSpace(input))
        {
            // ReSharper disable once IdentifierTypo
            var unprocessableBarcode = new Barcode(aimId.BarcodeType, aimId.Modifier, barcodeExceptions);
            unprocessableBarcode.AddException(
                new BarcodeException(1007, Resources.Barcodes_Error_002, true),
                ParseStatus.Invalid);
            return unprocessableBarcode;
        }

        // Reorder 
        preProcessedData = aimId.Id.Length > 0 ? "]" + aimId.Id + input : input;

        // Determine the syntax of each record
        var recordFormats = new IsoIec15434Analyzer().Analyze(input, 0, out var messageHeader);

        // Convert UPC-E barcode data to a GTIN-14.

        if (!messageHeader)
        {
            // There is no ISO/IEC envelope, so check to see if the barcode symbology, if known, is supported by GS1 for AIs.
            if (aimId.BarcodeType != BarcodeType.NoIdentifier && aimId.BarcodeType != BarcodeType.Code128
                                                              && aimId.BarcodeType != BarcodeType.Gs1DataBar
                                                              && aimId.BarcodeType != BarcodeType.DataMatrix
                                                              && aimId.BarcodeType != BarcodeType.QrCode
                                                              && aimId.BarcodeType != BarcodeType.DotCode)
            {
                // Is this another GS1-supported barcode symbology used for GTINs?
                if (aimId.BarcodeType != BarcodeType.Interleaved2Of5 && aimId.BarcodeType != BarcodeType.UpcEan)
                {
                    // If not, return with error.
                    return GetUnsupportedBarcode();
                }

                if (aimId.Modifier == '4')
                {
                    // EAN-8 cannot carry GTIN-13/GTIN-14 compatible product codes, so is unsupported.
                    // Return with error.
                    return GetUnsupportedBarcode();
                }

                string TestForItf14() =>
                    input.Length == 14

                        // Assume this is an ITF-14 barcode and return the content as a product code,
                        ? "01" + input

                        // otherwise, return the input as-is
                        : input;

                // If the barcode type could be ITF-14,
                input = aimId.BarcodeType == BarcodeType.Interleaved2Of5

                            // Test if the barcode symbology is an ITF-14
                            ? TestForItf14()

                            // otherwise, if the barcode symbology is EAN-13, UPC-A, or UPC-E 13 digits without a supplement,
                            : aimId.Modifier switch
                              {
                                  // process the barcode data and return the product code,
                                  '0' => ProcessUpcOrEan13(input),

                                  // process the barcode data, strip off the supplement and return the product code,
                                  '3' => ProcessUpcOrEan13WithSupplement(),

                                  // otherwise, return the input as-is
                                  _ => input
                              };
            }

            if (aimId.BarcodeType == BarcodeType.NoIdentifier && input.All(c => c >= 48 && c <= 57))
            {
                var inputLength = input.Length;
                int[] upcEanSupplementSizes = { 15, 17, 18 };
                int[] upcEanSizes = { 13, 12, 8 };

                string TestItf14OrUpaCWithSupplement() =>
                    inputLength == 14

                        // Assume this is an ITF-14. However, it could be a UPC-A with 2 digit supplement.
                        ? "01" + input
                        : input;

                string TestUpcOrEan13WithSupplement() =>
                    upcEanSupplementSizes.Contains(inputLength)

                        // UPC-A with 2-digit supplement or EAN 13 with 2 or 5-digit supplement.
                        ? ProcessUpcOrEan13WithSupplement()
                        : TestItf14OrUpaCWithSupplement();

                input = upcEanSizes.Contains(inputLength)

                            // UPC-A, UPC-E or EAN 13. NB. we assume UPC-8 rather than EAN 8. EAN 8 cannot be transformed to GTIN-14
                            ? ProcessUpcOrEan13(input)
                            : TestUpcOrEan13WithSupplement();
            }

            Gs1Ai.Parser.Parse(input, ResolveGs1Entity, aimId.Id.Length > 0 ? 3 : 0);

            if (!barcode.Exceptions.Any())
            {
                // There were no exceptions
                return barcode;
            }

            // There were exceptions. Add an additional barcode exception to register this.
            barcode.AddException(
                new BarcodeException(1003, Resources.Barcodes_Error_004, false),
                !barcode.DataElements.Any() ? ParseStatus.Unrecognised : ParseStatus.Invalid);
            return barcode;

            void ResolveGs1Entity(IResolvedEntity resolvedIdentity)
            {
                ProcessResolvedGs1Entity(resolvedIdentity, barcode, 0);
            }
        }

        // Enumerate each record in the ISO/IEC 15434 envelope
        for (var recordIndex = 0; recordIndex < recordFormats.Count; recordIndex++)
        {
            var recordFormat = recordFormats[recordIndex];
            if (string.IsNullOrWhiteSpace(recordFormat.BarcodeData)) break;

            var offset = 0;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (((IsoIec15434Format)recordFormat).Format)
            {
                case FormatIndicator.Transportation:
                    offset = 3;
                    break;
                case FormatIndicator.Edi:
                case FormatIndicator.Text:
                    offset = 0;
                    break;
                case FormatIndicator.AscX12:
                case FormatIndicator.UnEdifact:
                    offset = 9;
                    break;
                case FormatIndicator.Gs1Ai:
                case FormatIndicator.AscMh10Di:
                case FormatIndicator.StructuredData:
                    offset = 1;
                    break;
                case FormatIndicator.Cii:
                    offset = 8;
                    break;
                case FormatIndicator.Binary:
                    offset = 1;
                    var record = input[recordFormat.StartPosition..];
                    if (!RegexBinary.IsMatch(record))
                    {
                        break;
                    }

                    var groups = RegexBinary.Match(record).Groups;
                    offset += groups["fileName"].Value.Length + 1;
                    offset += groups["compressionTechnique"].Value.Length + 1;
                    offset += groups["numberOfBytes"].Value.Length + 1;

                    break;

                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }

            var initialPosition = recordFormat.StartPosition + recordFormat.Indicator.Length + offset
                                + (aimId.Id.Length > 0 ? 3 : 0);

            if (recordFormat.Envelope != EnvelopeType.IsoIec15434
             || recordFormat.FormatScheme != FormatScheme.IsoIec15434)
            {
                // The record is invalid. Add a barcode exception to register this.
                barcode.AddException(
                    new BarcodeException(1004, Resources.Barcodes_Error_005, false),
                    ParseStatus.Invalid);
                continue;
            }

            var title = string.Empty;
            var description = string.Empty;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (((IsoIec15434Format)recordFormat).Format)
            {
                case FormatIndicator.AscMh10Di:

                    AnsiMhDi.Parser.Parse(recordFormat.BarcodeData, ResolveAscEntity, initialPosition);
                    break;

                    // Format 06 IFA-encoded (German pack)
                    void ResolveAscEntity(IResolvedEntity resolvedIdentity)
                    {
                        ProcessResolvedAscEntity(
                            resolvedIdentity,
                            barcode,

                            // ReSharper disable once AccessToModifiedClosure
                            recordIndex);
                    }
                case FormatIndicator.Gs1Ai:

                    Gs1Ai.Parser.Parse(recordFormat.BarcodeData, ResolveGs1Entity, initialPosition);
                    break;

                    // Format 05 GS1-encoded
                    void ResolveGs1Entity(IResolvedEntity resolvedIdentity)
                    {
                        ProcessResolvedGs1Entity(
                            resolvedIdentity,
                            barcode,

                            // ReSharper disable once AccessToModifiedClosure
                            recordIndex);
                    }
                case FormatIndicator.Text:
                    barcode.AddDataElement(
                        new TextEntity(
                            ((IsoIec15434Format)recordFormat).Format,
                            recordFormat.BarcodeData,
                            Resources.Barcodes_001,
                            Resources.Barcodes_002,
                            initialPosition,
                            recordIndex));
                    break;
                case FormatIndicator.Edi:
                    title = Resources.Barcodes_003;
                    description = Resources.Barcodes_004;
#pragma warning disable S907
                    goto case FormatIndicator.StructuredData;
#pragma warning restore S907
                case FormatIndicator.StructuredData:
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        title = Resources.Barcodes_005;
                        description = Resources.Barcodes_006;
                    }

                    barcode.AddDataElement(
                        new TextEntity(
                            ((IsoIec15434Format)recordFormat).Format,
                            recordFormat.BarcodeData,
                            title,
                            description,
                            initialPosition,
                            recordIndex));
                    barcode.AddException(
                        new BarcodeException(
                            1005,
                            string.Format(
                                CultureInfo.CurrentCulture,
                                Resources.Barcodes_Error_006,
                                ((IsoIec15434Format)recordFormat).Format.ToString()),
                            false),
                        ParseStatus.Ok);
                    break;
                case FormatIndicator.AscX12:
                    title = Resources.Barcodes_007;
                    description = Resources.Barcodes_008;
#pragma warning disable S907
                    goto case FormatIndicator.UnEdifact;
#pragma warning restore S907
                case FormatIndicator.UnEdifact:
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        // ReSharper disable once StringLiteralTypo
                        title = Resources.Barcodes_009;

                        // ReSharper disable once StringLiteralTypo
                        description = Resources.Barcodes_010;
                    }

                    var release = input[(recordFormat.StartPosition + 2)..(recordFormat.StartPosition + 5)];
                    var subRelease = input[(recordFormat.StartPosition + 5)..(recordFormat.StartPosition + 8)];
                    title = $"{title} [{release}{subRelease}]";

                    barcode.AddDataElement(
                        new StructuredDataEntity(
                            ((IsoIec15434Format)recordFormat).Format,
                            release,
                            subRelease,
                            recordFormat.BarcodeData,
                            title,
                            description,
                            initialPosition,
                            recordIndex));
                    barcode.AddException(
                        new BarcodeException(
                            1005,
                            string.Format(
                                CultureInfo.CurrentCulture,
                                Resources.Barcodes_Error_006,
                                ((IsoIec15434Format)recordFormat).Format.ToString()),
                            false),
                        ParseStatus.Ok);
                    break;
                case FormatIndicator.Cii:
                    var organisation = input[(recordFormat.StartPosition + 2)..(recordFormat.StartPosition + 6)];
                    var subOrganisation = input[(recordFormat.StartPosition + 6)..(recordFormat.StartPosition + 8)];
                    var edition = input[(recordFormat.StartPosition + 8)..(recordFormat.StartPosition + 10)];
                    title = string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Barcodes_011,
                        organisation,
                        subOrganisation,
                        edition);
                    description = Resources.Barcodes_012;

                    barcode.AddDataElement(
                        new CiiDataEntity(
                            ((IsoIec15434Format)recordFormat).Format,
                            organisation,
                            subOrganisation,
                            edition,
                            recordFormat.BarcodeData,
                            title,
                            description,
                            initialPosition,
                            recordIndex));
                    barcode.AddException(
                        new BarcodeException(
                            1005,
                            string.Format(
                                CultureInfo.CurrentCulture,
                                Resources.Barcodes_Error_006,
                                ((IsoIec15434Format)recordFormat).Format.ToString()),
                            false),
                        ParseStatus.Ok);
                    break;
                case FormatIndicator.Binary:
                    var record = input[recordFormat.StartPosition..];
                    if (!RegexBinary.IsMatch(record))
                    {
                        break;
                    }

                    var groups = RegexBinary.Match(record).Groups;
                    title = string.Format(
                        CultureInfo.CurrentCulture,
                        Resources.Barcodes_013,
                        groups["fileName"].Value,
                        groups["compressionTechnique"].Value,
                        groups["numberOfBytes"].Value);
                    description = Resources.Barcodes_014;
                    barcode.AddDataElement(
                        new BinaryEntity(
                            ((IsoIec15434Format)recordFormat).Format,
                            groups["fileName"].Value,
                            groups["compressionTechnique"].Value,
                            int.TryParse(groups["numberOfBytes"].Value, out var numberOfBytes) ? numberOfBytes : 0,
                            recordFormat.BarcodeData,
                            title,
                            description,
                            initialPosition,
                            recordIndex));
                    break;
                default:
                    // The record format is not supported. Add an additional barcode exception to register this.
                    barcode.AddException(
                        new BarcodeException(1006, Resources.Barcodes_Error_007, false),
                        ParseStatus.Invalid);
                    break;
            }
        }

        if (!barcode.Exceptions.Any())
        {
            // There were no exceptions
            return barcode;
        }

        // There were exceptions. Add an additional barcode exception to register this.
        barcode.AddException(
            new BarcodeException(1003, Resources.Barcodes_Error_004, false),
            !barcode.DataElements.Any() ? ParseStatus.Unrecognised : ParseStatus.Invalid);
        return barcode;

        // Convert UPC-E barcode data to a GTIN-14.
        string ProcessUpcOrEan13WithSupplement()
        {
            input = input.Length switch
            {
                18 => input[..13],
                17 => input[..12],
                15 => input[..13],
                14 => input[..12],
                13 => input[..8],
                _  => input
            };

            return input.Length == 8 ? ProcessUpcOrEan13(input) : "01" + input.PadLeft(14, '0');
        }

        IBarcode GetUnsupportedBarcode()
        {
            // The barcode is not a recognised carrier of GS1 Application Identifiers or GTINs. This version of the
            // parser does not support the data format used in this barcode.
            var unsupportedBarcode = new Barcode(aimId.BarcodeType, aimId.Modifier);
            unsupportedBarcode.AddException(
                new BarcodeException(1002, Resources.Barcodes_Error_003, true),
                ParseStatus.Unrecognised);
            return unsupportedBarcode;
        }
    }

    private static string ProcessUpcOrEan13(string? input)
    {
        if (input.Length != 8)
        {
            return "01" + input.PadLeft(14, '0');
        }

        return input[6] switch
        {
            // XXNNN0 -> 0 or 1 + XX000-00NNN + check digit
            '0' => $"01{input[0]}{input[1..3]}{new string('0', 5)}{input[3..6]}{input[7]}".PadLeft(14, '0'),

            // XXNNN1 -> 0 or 1 + XX100-00NNN + check digit
            '1' => $"01{input[0]}{input[1..3]}1{new string('0', 4)}{input[3..6]}{input[7]}".PadLeft(14, '0'),

            // XXNNN2 -> 0 or 1 + XX200-00NNN + check digit
            '2' => $"01{input[0]}{input[1..3]}2{new string('0', 4)}{input[3..6]}{input[7]}".PadLeft(14, '0'),

            // XXXNN3 -> 0 or 1 + XXX00-000NN + check digit
            '3' => $"01{input[0]}{input[1..4]}{new string('0', 5)}{input[4..6]}{input[7]}".PadLeft(14, '0'),

            // XXXXN4 -> 0 or 1 + XXXX0-0000N + check digit
            '4' => $"01{input[0]}{input[1..5]}{new string('0', 5)}{input[5]}{input[7]}".PadLeft(14, '0'),

            // XXXXX5 -> 0 or 1 + XXXXX-00005 + check digit
            // XXXXX6 -> 0 or 1 + XXXXX-00006 + check digit
            // XXXXX7 -> 0 or 1 + XXXXX-00007 + check digit
            // XXXXX8 -> 0 or 1 + XXXXX-00008 + check digit
            // XXXXX9 -> 0 or 1 + XXXXX-00009 + check digit
            _ => $"01{input[0]}{input[1..6]}{new string('0', 4)}{input[6..]}".PadLeft(14, '0')
        };
    }

    /// <summary>
    ///   Delegated method for processing resolved ASC entities to populate a pack identifier object.
    /// </summary>
    /// <param name="resolvedIdentity">The resolved ASC entity.</param>
    /// <param name="barcode">The barcode.</param>
    /// <param name="record">The index of the record.</param>
    private static void ProcessResolvedAscEntity(IResolvedEntity resolvedIdentity, IBarcode? barcode, int record)
    {
        if (barcode is null)
        {
            return;
        }

        var dataElement = new DataElement(
            FormatIndicator.AscMh10Di,
            resolvedIdentity.Entity,
            resolvedIdentity.Identifier,
            resolvedIdentity.Value,
            resolvedIdentity.DataTitle,
            resolvedIdentity.Description,
            resolvedIdentity.CharacterPosition,
            record);

        if (resolvedIdentity.IsError)
        {
            foreach (var exception in resolvedIdentity.Exceptions)
            {
                var dataElementException = new DataElementException(
                    exception.ErrorNumber,
                    exception.Message,
                    resolvedIdentity.DataTitle,
                    resolvedIdentity.CharacterPosition + exception.Offset,
                    resolvedIdentity.IsFatal);

                dataElement.AddException(dataElementException);
                barcode.AddException(dataElementException, ParseStatus.Invalid);
            }
        }

        barcode.AddDataElement(dataElement);
    }

    /// <summary>
    ///   Delegated method for processing resolved GS1 entities to populate a barcode object.
    /// </summary>
    /// <param name="resolvedIdentity">The resolved GS1 entity.</param>
    /// <param name="barcode">The barcode.</param>
    /// <param name="record">The index of the record.</param>
    private static void ProcessResolvedGs1Entity(IResolvedEntity resolvedIdentity, IBarcode? barcode, int record)
    {
        if (barcode is null)
        {
            return;
        }

        var dataElement = new DataElement(
            FormatIndicator.Gs1Ai,
            resolvedIdentity.Entity,
            resolvedIdentity.InverseExponent <= 0 ? resolvedIdentity.Identifier : resolvedIdentity.Identifier + resolvedIdentity.InverseExponent,
            resolvedIdentity.Value,
            resolvedIdentity.DataTitle,
            resolvedIdentity.Description,
            resolvedIdentity.CharacterPosition,
            record);

        if (resolvedIdentity.IsError)
        {
            foreach (var exception in resolvedIdentity.Exceptions)
            {
                var dataElementException = new DataElementException(
                    exception.ErrorNumber,
                    exception.Message,
                    resolvedIdentity.DataTitle,
                    resolvedIdentity.CharacterPosition + exception.Offset,
                    resolvedIdentity.IsFatal);

                dataElement.AddException(dataElementException);
                barcode.AddException(dataElementException, ParseStatus.Invalid);
            }
        }

        barcode.AddDataElement(dataElement);
    }
}