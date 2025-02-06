// --------------------------------------------------------------------------
// <copyright file="AiTableEntry.cs" company="Solidsoft Reply Ltd.">
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
// Represents an AI Table entry.
// </summary>
// --------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.ElementStrings;

#if NETCOREAPP
using System.Text.Json.Serialization;
#endif

using Newtonsoft.Json;

/// <summary>
/// Represents an AI Table entry.
/// </summary>
public record AiTableEntry() {

    /* Cannot use a primary constructor here because the Microsoft JSON serialiser does not support property
     * name attributes on constructor parameters (NewtonSoft.Json does) - January 2025.
     */

    /// <summary>
    /// Initializes a new instance of the <see cref="AiTableEntry"/> class.
    /// </summary>
    /// <param name="description">The AI description.</param>
    /// <param name="ai">The AI.</param>
    /// <param name="format">Format specifier for the AI.</param>
    /// <param name="type">The AI type, in the context of a Digital Link.</param>
    /// <param name="predefinedLength">A value indicating whether the AI has a predefined length.</param>
    /// <param name="regex">A regular expression for validating the AI value.</param>
    /// <param name="title">The AI data title.</param>
    /// <param name="shortName">The short name for the AI (legacy feature).</param>
    /// <param name="checkDigitPosition">The position of the check digit.</param>
    /// <param name="qualifiers">A list of qualifier AIs (applies only to identifier AIs).</param>
    internal AiTableEntry(
        string description,
        string ai,
        string format,
        AiTypes type,
        bool predefinedLength,
        string regex,
        string? title = null,
        string? shortName = null,
        CheckDigitPosition? checkDigitPosition = null,
        List<string>? qualifiers = null)
        : this() {
            Description = description;
            Ai = ai;
            Format = format;
            Type = type;
            PredefinedLength = predefinedLength;
            Regex = regex;
            Title = title;
            ShortName = shortName;
            CheckDigitPosition = checkDigitPosition;
            Qualifiers = qualifiers;
    }

    /// <summary>
    ///   Gets the AI description.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("description")]
#endif
    [JsonProperty("description")]
#if NETCOREAPP
    public string Description { get; init; } = string.Empty;
#else
    public string Description { get; private set; } = string.Empty;
#endif

    /// <summary>
    /// Gets the AI.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("ai")]
#endif
    [JsonProperty("ai")]
#if NETCOREAPP
    public string Ai { get; init; } = string.Empty;
#else
    public string Ai { get; private set; } = string.Empty;
#endif

    /// <summary>
    /// Gets the format specifier for the AI.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("format")]
#endif
    [JsonProperty("format")]
#if NETCOREAPP
    public string Format { get; init; } = string.Empty;
#else
    public string Format { get; private set; } = string.Empty;
#endif

    /// <summary>
    /// Gets the AI type, in the context of a Digital Link.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("type")]
#endif
    [JsonProperty("type")]
#if NETCOREAPP
    public AiTypes Type { get; init; } = AiTypes.Identifier;
#else
    public AiTypes Type { get; private set; } = AiTypes.Identifier;
#endif

    /// <summary>
    /// Gets a value indicating whether the AI has a predefined length.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("predefinedLength")]
#endif
    [JsonProperty("predefinedLength")]
#if NETCOREAPP
    public bool PredefinedLength { get; init; } = false;
#else
    public bool PredefinedLength { get; private set; } = false;
#endif

    /// <summary>
    /// Gets a regular expression for validating the AI value.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("regex")]
#endif
    [JsonProperty("regex")]
#if NETCOREAPP
    public string Regex { get; init; } = string.Empty;
#else
    public string Regex { get; private set; } = string.Empty;
#endif

    /// <summary>
    /// Gets the AI data title.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("title")]
#endif
    [JsonProperty("title")]
#if NETCOREAPP
    public string? Title { get; init; } = null;
#else
    public string? Title { get; private set; }
#endif

    /// <summary>
    /// Gets the short name for the AI (legacy feature).
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("shortName")]
#endif
    [JsonProperty("shortName")]
#if NETCOREAPP
    public string? ShortName { get; init; } = null;
#else
    public string? ShortName { get; private set; }
#endif

    /// <summary>
    /// Gets the position of the check digit.
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("checkDigitPosition")]
#endif
    [JsonProperty("checkDigitPosition")]
#if NETCOREAPP
    public CheckDigitPosition? CheckDigitPosition { get; init; } = null;
#else
    public CheckDigitPosition? CheckDigitPosition { get; private set; }
#endif

    /// <summary>
    /// Gets a list of qualifier AIs (applies only to identifier AIs).
    /// </summary>
#if NETCOREAPP
    [JsonPropertyName("qualifiers")]
#endif
    [JsonProperty("qualifiers")]
#if NETCOREAPP
    public List<string>? Qualifiers { get; init; } = null;
#else
    public List<string>? Qualifiers { get; private set; }
#endif

    /// <summary>
    /// Returns the AI table entry as JSON.
    /// </summary>
    /// <returns>The AI table entry as JSON.</returns>
#pragma warning disable VSSpell001 // Spell Check
    public string ToJson() =>
#if NETCOREAPP
        System.Text.Json.JsonSerializer.Serialize(this);
#else
        JsonConvert.SerializeObject(this);
#endif
#pragma warning restore VSSpell001 // Spell Check

    /// <summary>
    /// Returns the AI table entry as JSON.
    /// </summary>
    /// <returns>The AI table entry as JSON.</returns>
    public override string ToString() => ToJson();
}