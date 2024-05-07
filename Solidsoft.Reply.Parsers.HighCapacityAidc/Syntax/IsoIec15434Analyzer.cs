// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsoIec15434Analyzer.cs" company="Solidsoft Reply Ltd">
// Copyright (c) 2018-2024 Solidsoft Reply Ltd. All rights reserved.
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
// </copyright>
// <summary>
// Performs syntactic analysis of barcode data using ISO/IEC 15434 standards.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
///   Performs syntactic analysis of barcode data using ISO/IEC 15434 standards.
/// </summary>
#if NET7_0_OR_GREATER
public partial class IsoIec15434Analyzer : IAnalyzer {
#else
public class IsoIec15434Analyzer : IAnalyzer {
    /// <summary>
    ///   Regular expression that matches candidate format identifiers.
    /// </summary>
    private static readonly Regex MatchCandidatesRegEx = new (@"^((01\u001d\d{2})|(02|07)|((03|04)\d{6}\u001c?\u001d\u001f?)|((05|06|12)\u001d)|(08\d{8})|(09\u001d[\w\s]{0,30}\u001d[\w\s]{0,30}\u001d(0|\d{1,15})\u001d)).*$", RegexOptions.None);

    /// <summary>
    ///   Regular expression that matches the format identifier and preamble for binary data.
    /// </summary>
    private static readonly Regex BinaryRegEx = new (@"09\u001d(?<fileName>[\w\s]{0,30})\u001d(?<compressionTechnique>[\w\s]{0,30})\u001d(?<numberOfBytes>0|\d{1,15})\u001d.*", RegexOptions.None);
#endif

    /// <summary>
    ///   Analyze the syntactic format of each record in the barcode.
    /// </summary>
    /// <param name="data">The data to be analyzed.</param>
    /// <param name="characterPosition">The current character position.</param>
    /// <param name="messageHeader">Indicates if a message header was found.</param>
    /// <returns>A list of formats for each record in the barcode.</returns>
    public IList<IFormat> Analyze(string data, int characterPosition, out bool messageHeader) {
        messageHeader = false;

        // Is any data present?
        if (string.IsNullOrWhiteSpace(data)) {
            // Handle list for empty record
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            return [];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly
        }

        messageHeader = data.StartsWithOrdinal("[)>\u001e");

        var messageHeaderIncrement = messageHeader ? 4 : 0;
        characterPosition += messageHeaderIncrement;

        // Strip off the message header and footer. NB. the message footer is not always used, depending on record formats.
#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
        var records = messageHeader && data.Length >= 6
#if NET6_0_OR_GREATER
                          ? data[4..^TestEndCharacters()].Split((char)30)
#else
                          ? data.Substring(4, data.Length - TestEndCharacters() - 4).Split((char)30)
#endif
                          : [];
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

        int formatIdentifierRecordLength;

        // Get a list of format specifiers for the ISO/IEC 15434 barcode data.
        return records.Select(
                           (record, recordIndex) => record.Length >= 3
                                                        ? ResolveRecordFormat(record, recordIndex)
                                                        : new IsoIec15434Format(
                                                            string.Empty,
                                                            record,
                                                            recordIndex,
                                                            GetStartPosition(recordIndex)))
                      .Cast<IFormat>().ToList();

        static int GetFormatIdentifierRecordLength(string identifier, string record) {
            var recordFormat = Enum.Parse(typeof(FormatIndicator), identifier);

            switch (recordFormat) {
                case FormatIndicator.Transportation:
                    return 5;
                case FormatIndicator.Edi:
                case FormatIndicator.Text:
                    return 2;
                case FormatIndicator.AscX12:
                case FormatIndicator.UnEdifact:
                    return 11;
                case FormatIndicator.Gs1Ai:
                case FormatIndicator.AscMh10Di:
                case FormatIndicator.StructuredData:
                    return 3;
                case FormatIndicator.Cii:
                    return 10;
                case FormatIndicator.Binary:
                    var offset = 3;
#if NET7_0_OR_GREATER
                    if (!BinaryRegEx().IsMatch(record)) {
#else
                    if (!BinaryRegEx.IsMatch(record)) {
#endif
                        return offset;
                    }

#if NET7_0_OR_GREATER
                    var groups = BinaryRegEx().Match(record).Groups;
#else
                    var groups = BinaryRegEx.Match(record).Groups;
#endif
                    offset += groups["fileName"].Value.Length + 1;
                    offset += groups["compressionTechnique"].Value.Length + 1;
                    offset += groups["numberOfBytes"].Value.Length + 1;

                    return offset;
                default:
                    return 0;
            }
        }

        int TestEndCharacters() => data.EndsWithOrdinal("\u001e\u0004") ? 2 : 0;

        // Memoise the format identifier record length.
#pragma warning disable SA1113 // Comma should be on the same line as previous parameter
#pragma warning disable SA1115 // Parameter should follow comma
#pragma warning disable SA1001 // Commas should be spaced correctly
        int FormatIdentifierRecordLength(string record) =>
            formatIdentifierRecordLength =
#if NET6_0_OR_GREATER
                GetFormatIdentifierRecordLength(record[..2], record);
#else
                GetFormatIdentifierRecordLength(record.Substring(0, 2), record);
#endif

#pragma warning restore SA1115 // Parameter should follow comma
#pragma warning restore SA1113 // Comma should be on the same line as previous parameter

        // Create the format specifier for a valid format.
#pragma warning disable SA1118 // Parameter should not span multiple lines
        IsoIec15434Format CreateFormatSpecifier(string record, int recordIndex) =>

#if NET6_0_OR_GREATER
                new (record[..2], record.Length > FormatIdentifierRecordLength(record)
#else
                new (record.Substring(0, 2), record.Length > FormatIdentifierRecordLength(record)
#endif
#if NET6_0_OR_GREATER
                    ? record[formatIdentifierRecordLength..]
#else
                    ? record.Substring(formatIdentifierRecordLength)
#endif
                    : string.Empty,
                recordIndex,
                GetStartPosition(recordIndex));
#pragma warning restore SA1118 // Parameter should not span multiple lines
#pragma warning restore SA1001 // Commas should be spaced correctly

        // Resolve the record format and return a format specifier
        IsoIec15434Format ResolveRecordFormat(string record, int recordIndex) =>
#if NET7_0_OR_GREATER
            MatchCandidatesRegEx()
#else
            MatchCandidatesRegEx
#endif
            .IsMatch(record)
                ? CreateFormatSpecifier(record, recordIndex)
                : new IsoIec15434Format(
                    string.Empty,
                    string.Empty,
                    recordIndex,
                    GetStartPosition(recordIndex));

        // Get the start position for the record.
        int GetStartPosition(int recordIndex) =>
            characterPosition += recordIndex > 0

                // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
                ? (records?[recordIndex - 1].Length ?? 0) + 1
                : 0;
    }

#if NET7_0_OR_GREATER
    /// <summary>
    ///   Regular expression that matches candidate format identifiers.
    /// </summary>
    [GeneratedRegex(@"^((01\u001d\d{2})|(02|07)|((03|04)\d{6}\u001c?\u001d\u001f?)|((05|06|12)\u001d)|(08\d{8})|(09\u001d[\w\s]{0,30}\u001d[\w\s]{0,30}\u001d(0|\d{1,15})\u001d)).*$", RegexOptions.None, "en-US")]
    private static partial Regex MatchCandidatesRegEx();

    /// <summary>
    ///   Regular expression that matches the format identifier and preamble for binary data.
    /// </summary>
    [GeneratedRegex(@"09\u001d(?<fileName>[\w\s]{0,30})\u001d(?<compressionTechnique>[\w\s]{0,30})\u001d(?<numberOfBytes>0|\d{1,15})\u001d.*", RegexOptions.None, "en-US")]
    private static partial Regex BinaryRegEx();
#endif
}