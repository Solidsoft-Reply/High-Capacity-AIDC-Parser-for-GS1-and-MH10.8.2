// --------------------------------------------------------------------------
// <copyright file="AiTypes.cs" company="Solidsoft Reply Ltd.">
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
// Represents an AI type, as used in GS1 Digital Links.
// </summary>
// --------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.ElementStrings;

/// <summary>
/// Represents an AI type, as used in GS1 Digital Links.
/// </summary>
public enum AiTypes {
    /// <summary>
    /// AI used as a key identifier.
    /// </summary>
    Identifier,

    /// <summary>
    /// AI used as a qualifier to an identifier.
    /// </summary>
    Qualifier,

    /// <summary>
    /// AI used as a data attribute.
    /// </summary>
    DataAttribute,
}
