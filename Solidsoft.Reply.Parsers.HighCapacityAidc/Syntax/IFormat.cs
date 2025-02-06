// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFormat.cs" company="Solidsoft Reply Ltd">
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
// Represents the format of a barcode or barcode record.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax;

/// <summary>
///   Represents the format of a barcode or barcode record.
/// </summary>
public interface IFormat {
    /// <summary>
    ///   Gets the barcode or record data.
    /// </summary>
    string BarcodeData { get; }

    /// <summary>
    ///   Gets the envelope type.
    /// </summary>
    EnvelopeType Envelope { get; }

    /// <summary>
    ///   Gets the format scheme.
    /// </summary>
    FormatScheme FormatScheme { get; }

    /// <summary>
    ///   Gets the format indicator character(s).
    /// </summary>
    string Indicator { get; }

    /// <summary>
    ///   Gets the index of the record in the barcode. If there is no record structure,
    ///   returns -1.
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    // ReSharper disable once UnusedMember.Global
    int RecordIndex { get; }

    /// <summary>
    ///   Gets the character index of the start of the record, including the format indicator.
    /// </summary>
    int StartPosition { get; }
}