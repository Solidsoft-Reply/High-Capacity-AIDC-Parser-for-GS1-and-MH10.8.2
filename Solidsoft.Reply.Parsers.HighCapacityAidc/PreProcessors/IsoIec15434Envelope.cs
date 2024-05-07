// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsoIec15434Envelope.cs" company="Solidsoft Reply Ltd">
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
// Pre-processors for ISO/IEC 1434 envelope.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.PreProcessors;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using Common;
using Syntax;

/// <summary>
///   Pre-processors for ISO/IEC 1434 envelope.
/// </summary>
#if NET7_0_OR_GREATER
public static partial class IsoIec15434Envelope
#else
public static class IsoIec15434Envelope
#endif
{
#if !NET7_0_OR_GREATER
    /// <summary>
    ///   Returns a regular expression that matches candidate format identifiers.
    /// </summary>
    private static readonly Regex MatchCandidatesEdiTextCiiRecordsRegex = new(@"(?!\u001e).((01\u001d\d{2})|(02|07)|((03|04)\d{6}\u001c?\u001d\u001f?)|((05|06|12)\u001d)|(08\d{8})|(09\u001d[\w\s]{0,30}\u001d[\w\s]{0,30}\u001d(0|\d{1,15})\u001d))", RegexOptions.None);

    /// <summary>
    ///   Returns a regular expression that matches candidate format identifiers, but does not match EDI, Text or CII fields.
    /// </summary>
    private static readonly Regex MatchCandidatesNoEdiTextCiiRecordsRegex = new(@"(?!\u001e).((01\u001d\d{2})|((03|04)\d{6}\u001c?\u001d\u001f?)|((05|06|12)\u001d)|(09\u001d[\w\s]{0,30}\u001d[\w\s]{0,30}\u001d(0|\d{1,15})\u001d))", RegexOptions.None);
#endif

    /// <summary>
    ///   A best-endeavor pre-processor that fixes missing &lt;RS&gt; and &lt;EOT&gt;
    ///   characters in a barcode containing a single record in Format 05 or Format 06 (PPN).
    /// </summary>
    /// <param name="input">The barcode data input.</param>
    /// <param name="exceptions">Collection of exceptions.</param>
    /// <returns>The pre-processed barcode data input.</returns>
    public static string? FixMissingControlCharacters(string? input, out IList<PreprocessorException>? exceptions) {
        exceptions = null;
        return input is not null ? DoFixMissingControlCharacters(input) : null;
    }

#if NET7_0_OR_GREATER
    /// <summary>
    ///   Returns a regular expression that matches candidate format identifiers.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"(?!\u001e).((01\u001d\d{2})|(02|07)|((03|04)\d{6}\u001c?\u001d\u001f?)|((05|06|12)\u001d)|(08\d{8})|(09\u001d[\w\s]{0,30}\u001d[\w\s]{0,30}\u001d(0|\d{1,15})\u001d))", RegexOptions.None, "en-US")]
    private static partial Regex MatchCandidatesEdiTextCiiRecordsRegex();

    /// <summary>
    ///   Returns a regular expression that matches candidate format identifiers, but does not match EDI, Text or CII fields
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"(?!\u001e).((01\u001d\d{2})|((03|04)\d{6}\u001c?\u001d\u001f?)|((05|06|12)\u001d)|(09\u001d[\w\s]{0,30}\u001d[\w\s]{0,30}\u001d(0|\d{1,15})\u001d))", RegexOptions.None, "en -US")]
    private static partial Regex MatchCandidatesNoEdiTextCiiRecordsRegex();
#endif

    /// <summary>
    ///   A best-endeavor pre-processor that fixes missing &lt;RS&gt; and &lt;EOT&gt;
    ///   characters in a barcode containing a single record in Format 05 or Format 06 (PPN).
    /// </summary>
    /// <param name="input">The barcode data input.</param>
    /// <param name="matchCandidates">An optional regular expression.</param>
    /// <returns>The pre-processed barcode data input.</returns>
    private static string DoFixMissingControlCharacters(string input, Regex? matchCandidates = null) {
        // Pre-process for scanners that strip out <RS> and <EOT> characters.
        // This is a belt & braces approach. It only works with barcodes that contain a single record.
        const string malformedMessageHeader1 = "[)>06\u001d";
        const string malformedMessageHeader2 = "[)>05\u001d";
        const string malformedMessageHeader3 = "[)>\u000006\u001d";
        const string malformedMessageHeader4 = "[)>\u000005\u001d";
        const string malformedMessageHeader5 = "[)>\u001d06\u001d";
        const string malformedMessageHeader6 = "[)>\u001d05\u001d";
        const string messageHeader = "[)>";
        const string groupSeparator = "\u001d";
        const string recordSeparator = "\u001e";
        const string endOfTransmission = "\u0004";

        var unprocessedInput = input;
        var repeatNoTextEdi = matchCandidates is null;
#if NET7_0_OR_GREATER
        matchCandidates ??= MatchCandidatesEdiTextCiiRecordsRegex();
#else
        matchCandidates ??= MatchCandidatesEdiTextCiiRecordsRegex;
#endif

        var noRecordSeparatorDetected = false;

        if (!input.StartsWithOrdinal(messageHeader)) {
            return input;
        }

        if (input.StartsWithOrdinal(malformedMessageHeader1)
         || input.StartsWithOrdinal(malformedMessageHeader2)) {
            noRecordSeparatorDetected = true;

            // Correct the malformed message header
#if NET6_0_OR_GREATER
            input = messageHeader + recordSeparator + input[messageHeader.Length..];
#else
            input = messageHeader + recordSeparator + input.Substring(messageHeader.Length);
#endif
        }
        else if (input.StartsWithOrdinal(malformedMessageHeader3)
              || input.StartsWithOrdinal(malformedMessageHeader4)) {
            noRecordSeparatorDetected = true;

            // The scanner is emitting a null character instead of a record separator.
#if NET6_0_OR_GREATER
            input = messageHeader + recordSeparator + input[(messageHeader.Length + 1) ..]
#else
            input = messageHeader + recordSeparator + input.Substring(messageHeader.Length + 1)
#endif
#if NET2_0_OR_GREATER
                .Replace(
                        "\u0000",
                        recordSeparator
                        , StringComparison.Ordinal);
#else
                .Replace(
                    "\u0000",
                    recordSeparator);

#endif
        }
        else if (input.StartsWithOrdinal(malformedMessageHeader5)
              || input.StartsWithOrdinal(malformedMessageHeader6)) {
            // The scanner is emitting a null character instead of a record separator, but this has been
            // converted to an ASCII 29 because ASCII 29s are also emitted as null characters.
#if NET6_0_OR_GREATER
            input = messageHeader + recordSeparator + input[(messageHeader.Length + 1) ..];
#else
            input = messageHeader + recordSeparator + input.Substring(messageHeader.Length + 1);
#endif

            if (input.EndsWithOrdinal(groupSeparator + endOfTransmission)
             || input.EndsWithOrdinal(groupSeparator)) {
                input =
#if NET6_0_OR_GREATER
                    $"{input[..input.LastIndexOf(groupSeparator, StringComparison.Ordinal)]}{recordSeparator}{endOfTransmission}";
#else
                    $"{input.Substring(0, input.LastIndexOf(groupSeparator, StringComparison.Ordinal))}{recordSeparator}{endOfTransmission}";
#endif
            }

#if NET2_0_OR_GREATER
            input = input.Replace(
                $"{groupSeparator}05{groupSeparator}",
                $"{recordSeparator}05{groupSeparator}"
                , StringComparison.Ordinal);
            input = input.Replace(
                $"{groupSeparator}06{groupSeparator}",
                $"{recordSeparator}06{groupSeparator}"
                , StringComparison.Ordinal);
#else
            input = input.Replace(
                    $"{groupSeparator}05{groupSeparator}",
                    $"{recordSeparator}05{groupSeparator}");
            input = input.Replace(
                    $"{groupSeparator}06{groupSeparator}",
                    $"{recordSeparator}06{groupSeparator}");
#endif
        }
#if NET6_0_OR_GREATER
        else if (input[4..7] == $"05{groupSeparator}" || input[4..7] == $"06{groupSeparator}")
#else
        else if (input.Substring(4, 3) == $"05{groupSeparator}" || input.Substring(4, 3) == $"06{groupSeparator}")
#endif
        {
            var ascii30Character = input[3];

            // ASCII 29 is supported, but we need to check if ASCII 30 is mapped to another character
            if (ascii30Character != '\x001e') {
                // ASCII 30 is mapped to another character
#if NET6_0_OR_GREATER
                input = messageHeader + recordSeparator + input[(messageHeader.Length + 1) ..];
#else
                input = messageHeader + recordSeparator + input.Substring(messageHeader.Length + 1);
#endif

                if (input.EndsWithOrdinal(ascii30Character + endOfTransmission)
                 || input.EndsWithOrdinal(groupSeparator)) {
                    var lastElementCharPos = input.LastIndexOf(ascii30Character);
#if NET6_0_OR_GREATER
                    input = $"{input[..lastElementCharPos]}{groupSeparator}{recordSeparator}{endOfTransmission}";
#else
                    input = $"{input.Substring(0, lastElementCharPos)}{groupSeparator}{recordSeparator}{endOfTransmission}";
#endif
                }

#if NET2_0_OR_GREATER
                input = input.Replace(
                    $"{ascii30Character}05{groupSeparator}",
                    $"{recordSeparator}05{groupSeparator}"
                , StringComparison.Ordinal);
#else
                input = input.Replace(
                        $"{ascii30Character}05{groupSeparator}",
                        $"{recordSeparator}05{groupSeparator}");
#endif

#if NET2_0_OR_GREATER
                input = input.Replace(
                    $"{ascii30Character}06{groupSeparator}",
                    $"{recordSeparator}06{groupSeparator}"
                , StringComparison.Ordinal);
#else
                input = input.Replace(
                    $"{ascii30Character}06{groupSeparator}",
                    $"{recordSeparator}06{groupSeparator}");
#endif
            }
        }

        if (!input.EndsWithOrdinal(endOfTransmission)) {
            if (!input.EndsWithOrdinal(recordSeparator)) {
                if (input.EndsWithOrdinal("\u0000")) {
#if NET6_0_OR_GREATER
                    input = input[..^1];
#else
                    input = input.Substring(0, input.Length - 1);
#endif
                }

                // Add a format trailer
                input += recordSeparator;
            }

            // Add a message trailer
            input += endOfTransmission;
        }
        else if (!input.EndsWithOrdinal(recordSeparator + endOfTransmission)) {
#if NET6_0_OR_GREATER
            input = $"{input[..^endOfTransmission.Length]}{recordSeparator}{endOfTransmission}";
#else
            input = $"{input.Substring(0, endOfTransmission.Length)}{recordSeparator}{endOfTransmission}";
#endif
        }

        if (noRecordSeparatorDetected) {
            /* Perform a brute-force in-depth analysis to determine if there is a
             * single, unambiguous, error-free interpretation of the data. If the
             * interpretation is ambiguous or any errors occur the code will
             * indicate that the pack identifier should not be submitted to the
             * National System. There remains a potential risk that the barcode
             * data could contain errors, but that a single, unambiguous, error-
             * free interpretation of the data is possible due to the lack of
             * record separators. However, no such candidate error has been
             * identified and, even if this could occur, the approach taken here
             * means that the same problem would be encountered in scenarios where
             * the barcode scanner reports Record Separators. Therefore, the pack
             * identifier can responsibly be submitted to the National System.
             * */
            var candidates = new List<string>();

            // Divide the input into sections
            var sections = new List<string>();
            var currentPos = 0;

            foreach (var index in matchCandidates.Matches(input)
#if !NET6_0_OR_GREATER
                        .Cast<Match>()
#endif
                        .Select(match => match.Index)) {
#if NET6_0_OR_GREATER
                sections.Add(input[currentPos.. (index + 1)]);
#else
                sections.Add(input.Substring(currentPos, index + 1 - currentPos));
#endif
                currentPos = index + 1;
            }

#if NET5_0_OR_GREATER
            sections.Add(input[currentPos..]);
#else
            sections.Add(input.Substring(currentPos));
#endif

            void IdentifyCandidates(int pos, string preamble) {
                if (pos == sections.Count) {
                    return;
                }

                candidates.Add(
                    preamble + sections.GetRange(pos, sections.Count - pos).Aggregate((s1, s2) => s1 + s2));

                var sectionBuilder = new StringBuilder(preamble);

                for (var idx = pos + 1; idx < sections.Count; idx++) {
                    sectionBuilder.Append(sections[idx - 1]);
                    IdentifyCandidates(idx, sectionBuilder + recordSeparator);
                }
            }

            IdentifyCandidates(0, string.Empty);
            string candidateData;
            var recordErrors = new Dictionary<string, bool>();

            void ProcessFormatRecord(IResolvedEntity resolvedEntity) {
                // ReSharper disable once AccessToModifiedClosure
                // ReSharper disable once InlineTemporaryVariable
                var key = candidateData;

                if (string.IsNullOrWhiteSpace(key)) {
                    return;
                }

                // Add the candidate string to the dictionary and set the error flags
#if NET2_0_OR_GREATER
                            recordErrors.TryAdd(key, false);
#else
                try {
                    recordErrors.Add(key, false);
                }
                catch (Exception) {
                    // Do nothing
                }
#endif

                if (!resolvedEntity.IsError && !resolvedEntity.Exceptions.Any()) {
                    return;
                }

                recordErrors[key] = resolvedEntity.Entity switch {
                    20001 when resolvedEntity.Identifier == "1T" =>

                        // We will make a special case for batch identifiers. IFA does not
                        // follow the MH10.8.2 standard here
                        !(resolvedEntity.Exceptions.Count() == 2
                       && resolvedEntity.Exceptions.Any(re => re.ErrorNumber == 3005)
                       && resolvedEntity.Exceptions.Any(re => re.ErrorNumber == 3100)),
                    19000 when resolvedEntity.Identifier == "S" =>

                        // We will make a special case for serial numbers. IFA does not
                        // follow the MH10.8.2 standard here
                        !(resolvedEntity.Exceptions.Count() == 2
                       && resolvedEntity.Exceptions.Any(re => re.ErrorNumber == 3005)
                       && resolvedEntity.Exceptions.Any(re => re.ErrorNumber == 3100)),
                    _ => true
                };
            }

#pragma warning disable S4158 // Empty collections should not be accessed or iterated
            foreach (var candidate in candidates) {
                candidateData = candidate;

                // Determine the number of detected records
                var recordFormats = new IsoIec15434Analyzer().Analyze(candidate, 0, out _);

                foreach (var recordFormat in recordFormats) {
                    switch (recordFormat.Indicator) {
                        case "05":
                            Gs1Ai.Parser.Parse(recordFormat.BarcodeData, ProcessFormatRecord);
                            break;
                        case "06":
                            AnsiMhDi.Parser.Parse(recordFormat.BarcodeData, ProcessFormatRecord);
                            break;

                        // ReSharper disable once RedundantEmptySwitchSection
                        default:
                            break;
                    }
                }
            }
#pragma warning restore S4158 // Empty collections should not be accessed or iterated

            /*
             * This is the only place where we relax the strict rules concerning a single,
             * unambiguous, error-free interpretation of the data. If there is more than
             * one valid interpretation, and the first candidate is valid, then
             * we will accept this. The first candidate is always the input string
             * reported by the scanner. If we didn't do this, a significant number of
             * German packs would be un-processable. There is a risk here, but it is
             * minimal.
             */

            // There is more than one candidate.
            // Either all candidates have errors, or more than one candidate has no errors.
            // If there are multiple valid candidates, we will use the first candidate (the
            // data reported by the scanner) if it is valid. We won't accept any other valid
            // candidate as this is too ambiguous.
            string TestIfFirstCandidateIsValid() =>
                (!recordErrors.FirstOrDefault().Value

                    // If the first (not pre-processed) value parses OK,
                    // we will accept it. */
                    ? candidates.FirstOrDefault()
                    : string.Empty) ?? string.Empty;

            // Either all candidates have errors, or more than one candidate has no errors.
            // Test for existence of single candidate, only. This must be a candidate with errors.
            string TestForSingleCandidateWithError() =>
                recordErrors.Count == 1
                    ? string.Empty
                    : TestIfFirstCandidateIsValid();

            // If one single candidate has no errors, accept this.
            input = recordErrors.Values.Count(value => !value) == 1
                        ? recordErrors.First(kvp => !kvp.Value).Key
                        : TestForSingleCandidateWithError();

            // If we failed, try again, but without taking into account the EDI, text or CII
            // fields which don't use ASCII 29 delimiters after the format indicator.
            input = string.IsNullOrWhiteSpace(input) && repeatNoTextEdi

#if NET7_0_OR_GREATER
                ? DoFixMissingControlCharacters(unprocessedInput, MatchCandidatesNoEdiTextCiiRecordsRegex())
#else
                ? DoFixMissingControlCharacters(unprocessedInput, MatchCandidatesNoEdiTextCiiRecordsRegex)
#endif
                : input;
        }

        // Fix remaining issues with missing characters for formats 03 and 04
        var inputRecordFormats = new IsoIec15434Analyzer().Analyze(input, 0, out _);

        foreach (var recordFormat in inputRecordFormats) {
            switch (recordFormat.Indicator) {
                case "03":
                case "04":
                    var indexOfGroupSeparator = recordFormat.StartPosition + 8;
#if NET5_0_OR_GREATER
                    var threeCharacters = input[indexOfGroupSeparator.. (indexOfGroupSeparator + 3)];
#else
                    var threeCharacters = input.Substring(indexOfGroupSeparator, 3);
#endif
                    var lastChar =
                        threeCharacters.LastOrDefault(c => c is '\u001c' or '\u001d' or '\u001f');
                    var indexOfStartOfValue =
                        indexOfGroupSeparator + threeCharacters.LastIndexOf(lastChar) + 1;
                    input =
#if NET5_0_OR_GREATER
                        $"{input[..indexOfGroupSeparator]}\u001c\u001d\u001f{input[indexOfStartOfValue..]}";
#else
                        $"{input.Substring(0, indexOfGroupSeparator)}\u001c\u001d\u001f{input.Substring(indexOfStartOfValue)}";
#endif
                    break;

                // ReSharper disable once RedundantEmptySwitchSection
                default:
                    break;
            }
        }

        return input;
    }
}