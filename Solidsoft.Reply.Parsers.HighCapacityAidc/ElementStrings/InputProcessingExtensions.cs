// --------------------------------------------------------------------------
// <copyright file="InputProcessingExtensions.cs" company="Solidsoft Reply Ltd.">
// Copyright © 2025 Solidsoft Reply Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at

// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <summary>
// A table of GS1 Application Identifier specifications.
// </summary>
// --------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.ElementStrings;

using Solidsoft.Reply.Parsers.HighCapacityAidc.Properties;

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

/// <summary>
/// Extension methods for processing input strings.
/// </summary>
internal static partial class InputProcessingExtensions
{
    /// <summary>
    /// A read-only list of AIs that require termination using the FNC1 character.
    /// </summary>
    private static readonly AiTable _aiTable = AiTable.Create();

#if !NET7_0_OR_GREATER
    /// <summary>
    ///   Regular expression that matches candidate format identifiers.
    /// </summary>
    private static readonly Regex RegexBracketedAiParser = new (@"\((\d{2,4}?)\)", RegexOptions.None);

    /// <summary>
    ///   Regular expression that matches the format identifier and preamble for binary data.
    /// </summary>
    private static readonly Regex RegexInitialParenthesisedAiDetector = new (@"^\((\d{2,4}?)\)", RegexOptions.None);
#endif

    /// <summary>
    /// Gets the table of GS1 Application Identifiers with predefined lengths.
    /// </summary>
    private static PredefinedLengthTable _predefinedLengthTable = PredefinedLengthTable.Create();

    /// <summary>
    /// Converts a bracketed element string to a FNC1 string using ASCII 29.
    /// </summary>
    /// <param name="input">A bracketed element string.</param>
    /// <param name="noValidation">
    /// If true, the element string is not validated. The GS1 AI dictionary may contain invalid AIs and AI values.
    /// </param>
    /// <returns>A FNC1 element string.</returns>
    /// <exception cref="BarcodeException">Invalid syntax.</exception>
    public static string ConvertParenthesesAIsToFnc1(
        this string input,
        bool noValidation)
    {
        // This regex finds sequences like (01), (3103), (3923), (10), etc.
#if NET7_0_OR_GREATER
        var aiPattern = RegexBracketedAiParser();
#else
        var aiPattern = RegexBracketedAiParser;
#endif
        var matches = aiPattern.Matches(input);

        if (matches.Count == 0)
        {
            // No AIs found.  Assume that the data is already in FNC1
            // format and return input as-is.
            return input;
        }

        var output = string.Empty;

        foreach (Match match in matches)
        {
            var ai = match.Groups[1].Value;  // The AI without parentheses
            var aiStart = match.Index;
            var aiLength = match.Length;

            // Extract the data that follows this AI up to the next AI or the end of the string
            var dataStart = aiStart + aiLength;
            int dataEnd; // We'll determine where the data ends

            // Find the next AI start (if any)
            Match nextAI = match.NextMatch();
            if (nextAI.Success)
            {
                dataEnd = nextAI.Index;
            }
            else
            {
                dataEnd = input.Length;
            }

#if NETCOREAPP
            var rawDataSection = input[dataStart..dataEnd];
#else
            var rawDataSection = input.Substring(dataStart, dataEnd - dataStart);
#endif

            // Determine if AI is fixed or variable length
            string dataForThisAI;

            var fnc1Elements = _aiTable.Where(e => !e.PredefinedLength).ToList();
            var isFnc1 = fnc1Elements.Any(e => e.Ai == ai);

            if (isFnc1)
            {
                // AI is FNC1
                dataForThisAI = rawDataSection;
            }
            else
            {
                var length = _predefinedLengthTable.ContainsKey(ai) ? _predefinedLengthTable[ai] - ai.Length : rawDataSection.Length;

                if (!noValidation && rawDataSection.Length < length)
                {
                    var message = string.Format(Resources.Barcodes_009, ai, rawDataSection, length);
                    throw new BarcodeException(1009, message, false);
                }
#if NETCOREAPP
                dataForThisAI = rawDataSection[..length];
#else
                dataForThisAI = rawDataSection.Substring(0, length);
#endif
            }

            output += ai + dataForThisAI + (isFnc1 ? "\x1D" : string.Empty);
        }

        // Strip off any trailing GS separator
        output = output.TrimEnd('\x1D');

        return output;
    }

    /// <summary>
    /// Check if the string has an initial AI that is enclosed within parentheses.
    /// </summary>
    /// <param name="elementString">The candidate element string.</param>
    /// <returns>True, if the string is a bracketed element string; otherwise false;</returns>
    public static bool IsBracketed(this string elementString)    {
        // Check if the string has an initial AI that is enclosed within parentheses
#if NET7_0_OR_GREATER
        var initialParenthesisedAiDetector = RegexInitialParenthesisedAiDetector();
#else
        var initialParenthesisedAiDetector = RegexInitialParenthesisedAiDetector;
#endif

        return initialParenthesisedAiDetector.IsMatch(elementString);
    }

#if NET7_0_OR_GREATER
    /// <summary>
    /// Regex that parses a bracketed element string.
    /// </summary>
    /// <returns>A regular expression.</returns>
    [GeneratedRegex(@"\((\d{2,4}?)\)", RegexOptions.Compiled)]
    private static partial Regex RegexBracketedAiParser();

    /// <summary>
    /// Regex that detects the presence of an initial parenthesised AI in a URI.
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(@"^\((\d{2,4}?)\)")]
    private static partial Regex RegexInitialParenthesisedAiDetector();
#endif
}