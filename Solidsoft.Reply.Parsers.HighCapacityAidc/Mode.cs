// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBarcode.cs" company="Solidsoft Reply Ltd">
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
// The mode of the parser. The default is ExtendedIsoIec15418.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

/// <summary>
/// The mode of the parser. The default is <see cref="ExtendedIsoIec15418"/>.
/// </summary>
public enum Mode {
    /// <summary>
    /// Default mode. If no AIM identifier is provided, the parser expects ISO/IEC 15418 formats (with or without an ISO/IEC 15343 envelope), 
    /// but will attempt to interpret the data as a GS1 GTIN if this fails.
    /// if this fails.
    /// </summary>
    ExtendedIsoIec15418 = 0,

    /// <summary>
    /// The parser expects ISO/IEC 15418 formats (with or without an ISO/IEC 15343 envelope).
    /// </summary>
    IsoIec15418 = 1,

    /// <summary>
    /// The parser requires AIM identifiers and interprets the data accordingly.
    /// </summary>
    AimIdentifer = 2,
}