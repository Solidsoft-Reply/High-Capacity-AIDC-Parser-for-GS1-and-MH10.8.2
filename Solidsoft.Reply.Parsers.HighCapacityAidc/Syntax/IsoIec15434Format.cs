// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IsoIec15434Format.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2023 Solidsoft Reply Ltd. All rights reserved.
// </copyright>
// <license>
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
// </license>
// <summary>
// An ISO/IEC 15434 Format for a record within a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax;

using System;
using System.Globalization;

/// <summary>
///   An ISO/IEC 15434 Format for a record within a barcode.
/// </summary>
public class IsoIec15434Format : IFormat
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="IsoIec15434Format" /> class.
    /// </summary>
    /// <param name="indicator">
    ///   The format indicator.
    /// </param>
    /// <param name="barcodeData">
    ///   The barcode data.
    /// </param>

    // ReSharper disable once UnusedMember.Global
    public IsoIec15434Format(string indicator, string barcodeData)
    {
        Indicator = indicator;
        BarcodeData = barcodeData;
        RecordIndex = -1;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="IsoIec15434Format" /> class.
    /// </summary>
    /// <param name="indicator">
    ///   The format indicator.
    /// </param>
    /// <param name="barcodeData">
    ///   The barcode data.
    /// </param>
    /// <param name="recordIndex">
    ///   The record index.
    /// </param>
    /// <param name="startPosition">
    ///   The character position of the start of the record, including format indicator.
    /// </param>
    public IsoIec15434Format(string indicator, string barcodeData, int recordIndex, int startPosition)
    {
        Indicator = indicator;
        BarcodeData = barcodeData;
        RecordIndex = recordIndex;
        StartPosition = startPosition;
    }

    /// <summary>
    ///   Gets the barcode or record data.
    /// </summary>
    public string BarcodeData { get; }

    /// <summary>
    ///   Gets the envelope type.
    /// </summary>
    public EnvelopeType Envelope => EnvelopeType.IsoIec15434;

    /// <summary>
    ///   Gets the ISO/IEC 15434 format indicator.
    /// </summary>
    public FormatIndicator Format
    {
        get
        {
            if (Indicator.Length == 0)
            {
                return FormatIndicator.NoIndicator;
            }

            int indicatorNo;

            try
            {
                indicatorNo = Convert.ToInt32(Indicator, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                indicatorNo = 0;
            }
            catch (OverflowException)
            {
                indicatorNo = 0;
            }

            return indicatorNo switch
                   {
                       0  => FormatIndicator.NoIndicator,
                       1  => FormatIndicator.Transportation,
                       2  => FormatIndicator.Edi,
                       3  => FormatIndicator.AscX12,
                       4  => FormatIndicator.UnEdifact,
                       5  => FormatIndicator.Gs1Ai,
                       6  => FormatIndicator.AscMh10Di,
                       7  => FormatIndicator.Text,
                       8  => FormatIndicator.Cii,
                       9  => FormatIndicator.Binary,
                       12 => FormatIndicator.StructuredData,
                       _  => FormatIndicator.Unknown
                   };
        }
    }

    /// <summary>
    ///   Gets the format scheme.
    /// </summary>
    public FormatScheme FormatScheme => FormatScheme.IsoIec15434;

    /// <summary>
    ///   Gets the format indicator character(s)
    /// </summary>
    public string Indicator { get; }

    /// <inheritdoc />
    /// <summary>
    ///   Gets the index of the record in the barcode. If there is no record structure,
    ///   returns -1.
    /// </summary>
    public int RecordIndex { get; }

    /// <summary>
    ///   Gets the character index of the start of the record, including the format indicator.
    /// </summary>
    public int StartPosition { get; }
}