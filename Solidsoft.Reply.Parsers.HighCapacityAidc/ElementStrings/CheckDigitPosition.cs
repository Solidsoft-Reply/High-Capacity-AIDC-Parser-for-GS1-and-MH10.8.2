// --------------------------------------------------------------------------
// <copyright file="CheckDigitPosition.cs" company="Solidsoft Reply Ltd.">
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
// Represents the position of the check digit in a GS1 AI value.
// </summary>
// --------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.ElementStrings;

/// <summary>
/// Represents the position of the check digit in a GS1 AI value.
/// </summary>
public enum CheckDigitPosition {
    /// <summary>
    /// The check digit is the last character in the value.
    /// </summary>
    Last = -1,

    /// <summary>
    /// The check digit is the thirteenth character in the value.
    /// </summary>
    Pos13 = 13,

    /// <summary>
    /// The check digit is the fourteenth character in the value.
    /// </summary>
    Pos14 = 14,
}
