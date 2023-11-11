// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataElement.cs" company="Solidsoft Reply Ltd.">
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
// A data element in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

using System.Collections.Generic;

using Syntax;

/// <summary>
///   A data element in a barcode
/// </summary>
public class DataElement : IDataEntity
{
    /// <summary>
    ///   The list of exceptions when parsing the element data.
    /// </summary>
    private readonly List<DataElementException> _exceptions = new();

    /// <summary>
    ///   Initializes a new instance of the <see cref="DataElement" /> class.
    /// </summary>
    /// <param name="format">The ISO/IEC 15434 format indicator</param>
    /// <param name="entity">The entity number.</param>
    /// <param name="identifier">The data or application identifier.</param>
    /// <param name="data">The data in the barcode.</param>
    /// <param name="title">The element title.</param>
    /// <param name="description">A description of the data element.</param>
    /// <param name="position">The position of the first character of the data element.</param>
    /// <param name="record">The index of the record that contains the data element.</param>
    public DataElement(
        FormatIndicator format,
        int entity,
        string identifier,
        string data,
        string title,
        string description,
        int position,
        int record)
    {
        Format = format;
        Entity = entity;
        Identifier = identifier;
        Data = data;
        Title = title;
        Description = description;
        Position = position;
        Record = record;
    }

    /// <summary>
    ///   Gets the element data.
    /// </summary>
    public string Data { get; }

    /// <summary>
    ///   Gets the description of the data element.
    /// </summary>
    public string Description { get; }

    /// <summary>
    ///   Gets the element entity identifier.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int Entity { get; }

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    public IEnumerable<DataElementException> Exceptions => _exceptions;

    /// <summary>
    ///   Gets the format of the data element.
    /// </summary>
    public FormatIndicator Format { get; }

    /// <summary>
    ///   Gets the element identifier.
    /// </summary>
    public string Identifier { get; }

    /// <summary>
    ///   Gets the character position of the data element within the barcode data.
    /// </summary>
    public int Position { get; }

    /// <summary>
    ///   Gets the index of the record in the barcode. Zero-based.
    /// </summary>
    public int Record { get; }

    /// <summary>
    ///   Gets the title of the data element.
    /// </summary>
    public string Title { get; }

    /// <summary>
    ///   Adds a parsing exception to the list.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public void AddException(DataElementException exception)
    {
        _exceptions.Add(exception);
    }
}