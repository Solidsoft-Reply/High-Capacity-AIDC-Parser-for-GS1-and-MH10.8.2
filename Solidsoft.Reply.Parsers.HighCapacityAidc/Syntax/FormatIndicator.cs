// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormatIndicator.cs" company="Solidsoft Reply Ltd">
// Copyright © 2018-2025 Solidsoft Reply Ltd. All rights reserved.
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
// ISO/IEC 15434 Format indicators.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CommentTypo
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax;

/// <summary>
///   ISO/IEC 15434 Format indicators.
/// </summary>
public enum FormatIndicator {
    /// <summary>
    ///   No format indicator was provided.
    /// </summary>
    NoIndicator = 0,

    /// <summary>
    ///   Transportation: &lt;GS&gt;dd
    /// </summary>
    Transportation = 1,

    /// <summary>
    ///   Complete EDI message / transaction: 02
    /// </summary>
    Edi = 2,

    /// <summary>
    ///   ASC 12: 03nnnnnn&lt;FS&gt;&gt;&lt;GS&gt;&lt;US&gt;
    /// </summary>
    AscX12 = 3,

    /// <summary>
    ///   UN/EDIFACT: 04nnnnnn&lt;FS&gt;&lt;GS&gt;&lt;US&gt;
    /// </summary>
    // ReSharper disable once IdentifierTypo
    UnEdifact = 4,

    /// <summary>
    ///   GS1 AIs: 05&lt;GS&gt;
    /// </summary>
    Gs1Ai = 5,

    /// <summary>
    ///   ASC MH10 DIs: 06&lt;GS&gt;
    /// </summary>
    AscMh10Di = 6,

    /// <summary>
    ///   Text: 07
    /// </summary>
    Text = 7,

    /// <summary>
    ///   CII - Center for Informatization of Industry - Japan: 08nnnnnnnn
    /// </summary>
    Cii = 8,

    /// <summary>
    ///   Binary: 09&lt;GS&gt;x1..30&lt;GS&gt;x1..30&lt;GS&gt;b1..15&lt;GS&gt;
    /// </summary>
    Binary = 9,

    /// <summary>
    ///   Structured Data: 12&lt;GS&gt;
    /// </summary>
    StructuredData = 12,

    /// <summary>
    ///   An Unknown format indicator was provided.
    /// </summary>
    Unknown = int.MaxValue,
}