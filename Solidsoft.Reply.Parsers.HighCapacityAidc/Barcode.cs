// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Barcode.cs" company="Solidsoft Reply Ltd">
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
// The barcode and list of data elements.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using BarcodeScanner.Symbology;

/// <summary>
///   The barcode and list of data elements.
/// </summary>
public class Barcode : IBarcode {
    /// <summary>
    ///   The list of data elements in the barcode.
    /// </summary>
    private readonly List<IDataEntity> _dataElements = [];

    /// <summary>
    ///   The list of exceptions when parsing the barcode data.
    /// </summary>
    private readonly List<BarcodeException> _exceptions = [];

    /// <summary>
    ///   Initializes a new instance of the <see cref="Barcode" /> class.
    /// </summary>
    /// <param name="barcodeType">The barcode type.</param>
    /// <param name="modifier">The barcode type modifier, if any.</param>
    /// <param name="exceptions">A list of exceptions.</param>
    public Barcode(BarcodeType barcodeType, char? modifier = null, IList<BarcodeException>? exceptions = null) {
        BarcodeType = barcodeType;
        BarcodeTypeModifier = modifier ?? char.MinValue;
        IsValid = true;
        IsRecognised = true;

        if (exceptions == null) return;

        foreach (var exception in exceptions) {
            _exceptions.Add(exception);
        }
    }

    /// <summary>
    ///   Gets the barcode type.
    /// </summary>
    public BarcodeType BarcodeType { get; }

    /// <summary>
    ///   Gets the barcode type modifier.
    /// </summary>
    public char BarcodeTypeModifier { get; }

    /// <summary>
    ///   Gets the data elements within the barcode.
    /// </summary>
    public IEnumerable<IDataEntity> DataElements => _dataElements;

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    public IEnumerable<BarcodeException> Exceptions => _exceptions;

    /// <summary>
    ///   Gets a value indicating whether the barcode is recognised.
    /// </summary>
    // ReSharper disable once IdentifierTypo
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
    public bool IsRecognised { get; private set; }

    /// <summary>
    ///   Gets a value indicating whether the barcode is valid.
    /// </summary>
    public bool IsValid { get; private set; }

    /// <summary>
    ///   Adds a data element to the list.
    /// </summary>
    /// <param name="entity">The data element.</param>
    public void AddDataElement(IDataEntity entity) {
        _dataElements.Add(entity);
    }

    /// <summary>
    ///   Adds a parsing exception to the list.
    /// </summary>
    /// <param name="exception">The exception.</param>
    /// <param name="parseStatus">The parse status of the barcode, based on this exception.</param>
    public void AddException(BarcodeException exception, ParseStatus parseStatus) {
        _exceptions.Add(exception);

        if (parseStatus == ParseStatus.Invalid) {
            IsValid = false;
            return;
        }

        if (parseStatus != ParseStatus.Unrecognised) {
            return;
        }

        IsRecognised = false;
    }
}