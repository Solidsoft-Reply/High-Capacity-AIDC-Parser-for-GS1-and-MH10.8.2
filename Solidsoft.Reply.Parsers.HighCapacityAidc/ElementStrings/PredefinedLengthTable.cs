// --------------------------------------------------------------------------
// <copyright file="_predefinedLengthTable.cs" company="Solidsoft Reply Ltd.">
// Copyright © 2025 Solidsoft Reply Ltd. All rights reserved.
//
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
// Represents the table of predefined length AIs.
// </summary>
// --------------------------------------------------------------------------
// The code in this file is derived in part from the GS1 Digital Link
// Compression Prototype which is licenced under the Apache License,
// Version 2.0.
//
// Copyright 2019 GS1 AISBL
//
// See https://github.com/gs1/GS1DigitalLinkCompressionPrototype for
// further details.
// --------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.ElementStrings;

using System.Collections;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Represents the table of predefined length AIs.
/// </summary>
/// <remarks>
/// Element Strings with predefined length using GS1 Application Identifiers, as
/// defined in GS1 Gen Specs - see Figure 7.8.4-2 of GS1 Gen Specs v18 at
/// https://www.gs1.org/docs/barcodes/GS1_General_Specifications.pdf.
/// </remarks>
internal class PredefinedLengthTable : IReadOnlyDictionary<string, int> {

    /// <summary>
    /// The predefined length table.
    /// </summary>
    private static readonly IDictionary<string, int> _predefinedLengthTable;

    /// <summary>
    /// Initializes static members of the <see cref="PredefinedLengthTable"/> class.
    /// </summary>
    static PredefinedLengthTable() {
        _predefinedLengthTable = new Dictionary<string, int>() {
            { "00", 20 },
            { "01", 16 },
            { "02", 16 },
            { "03", 16 },
            { "04", 18 },
            { "11", 8 },
            { "12", 8 },
            { "13", 8 },
            { "14", 8 },
            { "15", 8 },
            { "16", 8 },
            { "17", 8 },
            { "18", 8 },
            { "19", 8 },
            { "20", 4 },
            { "31", 10 },
            { "32", 10 },
            { "33", 10 },
            { "34", 10 },
            { "35", 10 },
            { "36", 10 },
            { "41", 16 }
        };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="PredefinedLengthTable"/> class.
    /// </summary>
    private PredefinedLengthTable() {
    }

    /// <summary>
    /// Gets a collection containing the keys in the dictionary.
    /// </summary>
    public IEnumerable<string> Keys => _predefinedLengthTable.Keys;

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    public IEnumerable<int> Values => _predefinedLengthTable.Values;

    /// <summary>
    /// Gets the number of elements contained in the dictionary.
    /// </summary>
    public int Count => _predefinedLengthTable.Count;

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>The length of the AI value.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public int this[string key] => _predefinedLengthTable[key];

    /// <summary>
    /// Factory method to create a new <see cref="PredefinedLengthTable"/> instance.
    /// </summary>
    /// <returns>An <see cref="PredefinedLengthTable"/> instance.</returns>
    public static PredefinedLengthTable Create() => [];

    /// <summary>
    /// Determines whether the dictionary contains an element with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <returns>True, if the dictionary contains the key; otherwise false.</returns>
    public bool ContainsKey(string key) => _predefinedLengthTable.ContainsKey(key);

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator for the dictionary.</returns>
    public IEnumerator<KeyValuePair<string, int>> GetEnumerator() => _predefinedLengthTable.GetEnumerator();

    /// <summary>
    /// Gets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The AI key.</param>
    /// <param name="value">The length of the AI value.</param>
    /// <returns>True, if the key was found; otherwise false.</returns>
#if NETCOREAPP
    public bool TryGetValue(string key, [MaybeNullWhen(false)] out int value) => _predefinedLengthTable.TryGetValue(key, out value);
#else
    public bool TryGetValue(string key, out int value) => _predefinedLengthTable.TryGetValue(key, out value);
#endif

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>The enumerator for the dictionary.</returns>
    IEnumerator IEnumerable.GetEnumerator() => _predefinedLengthTable.GetEnumerator();
}