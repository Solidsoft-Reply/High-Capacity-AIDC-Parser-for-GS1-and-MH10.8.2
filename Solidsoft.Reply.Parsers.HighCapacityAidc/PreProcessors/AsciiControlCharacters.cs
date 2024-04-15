// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AsciiControlCharacters.cs" company="Solidsoft Reply Ltd">
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
// Pre-processor methods for ASCII control characters characters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.PreProcessors;

using System.Text.RegularExpressions;

using System.Collections.Generic;
using Common;

/// <summary>
///   Pre-processor methods for ASCII control characters.
/// </summary>
public static class AsciiControlCharacters {
    /// <summary>
    ///   Replace representation of ASCII control characters with literals.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="exceptions">Collection of exceptions.</param>
    /// <returns>The processed string.</returns>
#pragma warning disable IDE0060
    public static string? ReplaceAngleBracketRepresentation(string? input, out IList<PreprocessorException>? exceptions)
#pragma warning restore IDE0060
    {
        exceptions = null;

        // Replace ASCII control characters represented as '<xx[x]>'
        return input?.ReplaceIgnoreCase("<FS>", ((char)28).ToInvariantString())
                     .ReplaceIgnoreCase("<GS>", ((char)29).ToInvariantString())
                     .ReplaceIgnoreCase("<RS>", ((char)30).ToInvariantString())
                     .ReplaceIgnoreCase("<US>", ((char)31).ToInvariantString())
                     .ReplaceIgnoreCase("<EOT>", ((char)04).ToInvariantString());
    }

    /// <summary>
    ///   Replaces one substring with another. The search is case-insensitive.
    /// </summary>
    /// <param name="input">The string to be searched.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The replacement string.</param>
    /// <returns>The input with all replacements made.</returns>
    private static string ReplaceIgnoreCase(this string input, string oldValue, string newValue) {
        return Regex.Replace(input, oldValue, newValue, RegexOptions.IgnoreCase);
    }
}