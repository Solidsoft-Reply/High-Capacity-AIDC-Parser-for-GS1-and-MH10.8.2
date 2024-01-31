// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBarcode.cs" company="Solidsoft Reply Ltd.">
//   (c) 2018-2024 Solidsoft Reply Ltd. All rights reserved.
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
// Represents a barcode and its content.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable UnusedMemberInSuper.Global


namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BarcodeScanner.Symbology;

/// <summary>
///   Represents a barcode and its content.
/// </summary>
public interface IBarcode
{
    /// <summary>
    ///   Gets the barcode type.
    /// </summary>
    BarcodeType BarcodeType { get; }

    /// <summary>
    ///   Gets the barcode type modifier (AIM).
    /// </summary>
    char BarcodeTypeModifier { get; }

    /// <summary>
    ///   Gets the data elements within the barcode.
    /// </summary>
    IEnumerable<IDataEntity> DataElements { get; }

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    IEnumerable<BarcodeException> Exceptions { get; }

    /// <summary>
    ///   Gets a value indicating whether the barcode is recognised.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
    bool IsRecognised { get; }

    /// <summary>
    ///   Gets a value indicating whether the barcode is valid.
    /// </summary>
    // ReSharper disable once UnusedMemberInSuper.Global
    // ReSharper disable once UnusedMember.Global
    bool IsValid { get; }

    /// <summary>
    ///   Adds a data element to the list.
    /// </summary>
    /// <param name="entity">The data element.</param>
    void AddDataElement(IDataEntity entity);

    /// <summary>
    ///   Adds a parsing exception to the list.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="parseStatus">The parse status of the barcode, based on this exception.</param>
    void AddException(BarcodeException exception, ParseStatus parseStatus);
}