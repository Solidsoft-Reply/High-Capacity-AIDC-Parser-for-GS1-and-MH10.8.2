// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CiiDataEntity.cs" company="Solidsoft Reply Ltd">
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
// A structured data entity in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434DataEntities;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

/// <summary>
///   A structured data entity in a barcode.
/// </summary>
/// <remarks>
///   Initializes a new instance of the <see cref="CiiDataEntity" /> class.
/// </remarks>
/// <param name="format">The ISO/IEC 15434 format indicator.</param>
/// <param name="organisation">The code for the organisation managing the standard used in the barcode.</param>
/// <param name="subOrganisation">The large classification code of the standard used in the barcode.</param>
/// <param name="edition">The version number of the standard used in the barcode.</param>
/// <param name="data">The data in the barcode.</param>
/// <param name="title">The entity title.</param>
/// <param name="description">A description of the data entity.</param>
/// <param name="position">The position of the first character of the data entity.</param>
/// <param name="record">The index of the record that contains the data entity.</param>
[method: SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]
public class CiiDataEntity(
        FormatIndicator format,
        string organisation,
        string subOrganisation,
        string edition,
        string data,
        string title,
        string description,
        int position,
        int record)
    : IDataEntity {
    /// <summary>
    ///   The list of exceptions when parsing the structured entity.
    /// </summary>
    private readonly List<DataElementException> _exceptions = [];

    /// <summary>
    ///   Gets the entity data.
    /// </summary>
    public string Data { get; } = data;

    /// <summary>
    ///   Gets the description of the data entity.
    /// </summary>
    public string Description { get; } = description;

    /// <summary>
    ///   Gets the version number of the standard used in the barcode.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int Edition { get; } = int.TryParse(edition, out var editionInt) ? editionInt : 0;

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    public IEnumerable<DataElementException> Exceptions => _exceptions;

    /// <summary>
    ///   Gets the format of the data element.
    /// </summary>
    public FormatIndicator Format { get; } = format;

    /// <summary>
    ///   Gets the code for the organisation managing the standard used in the barcode.
    /// </summary>
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1650:ElementDocumentationMustBeSpelledCorrectly",
        Justification = "Reviewed. Suppression is OK here.")]

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int Organisation { get; } = int.TryParse(organisation, out var organisationInt) ? organisationInt : 0;

    /// <summary>
    ///   Gets the character position of the data entity within the barcode data.
    /// </summary>
    public int Position { get; } = position;

    /// <summary>
    ///   Gets the index of the record in the barcode. Zero-based.
    /// </summary>
    public int Record { get; } = record;

    /// <summary>
    ///   Gets the large classification code of the standard used in the barcode.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int SubOrganisation { get; } = int.TryParse(subOrganisation, out var subOrganisationInt) ? subOrganisationInt : 0;

    /// <summary>
    ///   Gets the title of the data entity.
    /// </summary>
    public string Title { get; } = title;

    /// <summary>
    ///   Adds a parsing exception to the list.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public void AddException(DataElementException exception) {
        _exceptions.Add(exception);
    }
}