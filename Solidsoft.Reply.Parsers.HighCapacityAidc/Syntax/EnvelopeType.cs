﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnvelopeType.cs" company="Solidsoft Reply Ltd">
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
// The envelope type used in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax;

/// <summary>
///   The envelope type used in a barcode.
/// </summary>
public enum EnvelopeType {
    /// <summary>
    ///   The envelope type is unknown.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    Unknown,

    /// <summary>
    ///   ISO/IEC 15434.
    /// </summary>
    IsoIec15434,
}