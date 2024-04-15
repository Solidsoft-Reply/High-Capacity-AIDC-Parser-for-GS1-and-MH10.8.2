// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BinaryEntity.cs" company="Solidsoft Reply Ltd">
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
// A binary data entity in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax.IsoIec15434DataEntities;

using System.Collections.Generic;

/// <summary>
///   A binary data entity in a barcode.
/// </summary>
/// <remarks>
///   Initializes a new instance of the <see cref="BinaryEntity" /> class.
/// </remarks>
/// <param name="format">The ISO/IEC 15434 format indicator.</param>
/// <param name="fileType">The file type name.</param>
/// <param name="compressionTechnique">The compression technique name.</param>
/// <param name="numberOfBytes">The number of bytes.</param>
/// <param name="data">The data in the barcode.</param>
/// <param name="title">The entity title.</param>
/// <param name="description">A description of the data entity.</param>
/// <param name="position">The position of the first character of the data entity.</param>
/// <param name="record">The index of the record that contains the data entity.</param>
public class BinaryEntity(
        FormatIndicator format,
        string fileType,
        string compressionTechnique,
        int numberOfBytes,
        string data,
        string title,
        string description,
        int position,
        int record)
    : IDataEntity {
    /// <summary>
    ///   The list of exceptions when parsing the text entity.
    /// </summary>
    private readonly List<DataElementException> _exceptions = [];

    /// <summary>
    ///   Gets the compression technique name.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string CompressionTechnique { get; } = compressionTechnique;

    /// <summary>
    ///   Gets the entity data.
    /// </summary>
    public string Data { get; } = data;

    /// <summary>
    ///   Gets the description of the data entity.
    /// </summary>
    public string Description { get; } = description;

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    public IEnumerable<DataElementException> Exceptions => _exceptions;

    /// <summary>
    ///   Gets the file type name.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string FileType { get; } = fileType;

    /// <summary>
    ///   Gets the format of the data element.
    /// </summary>
    public FormatIndicator Format { get; } = format;

    /// <summary>
    ///   Gets the number of bytes.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public int NumberOfBytes { get; } = numberOfBytes;

    /// <summary>
    ///   Gets the character position of the data entity within the barcode data.
    /// </summary>
    public int Position { get; } = position;

    /// <summary>
    ///   Gets the index of the record in the barcode. Zero-based.
    /// </summary>
    public int Record { get; } = record;

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