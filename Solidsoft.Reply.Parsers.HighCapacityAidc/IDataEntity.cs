// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataEntity.cs" company="Solidsoft Reply Ltd.">
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
// Represents a data entity in a barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

using System.Collections.Generic;

using Syntax;

/// <summary>
///   Represents a data entity in a barcode.
/// </summary>
public interface IDataEntity
{
    /// <summary>
    ///   Gets the element data.
    /// </summary>
    string Data { get; }

    /// <summary>
    ///   Gets the description of the data element.
    /// </summary>
    string Description { get; }

    /// <summary>
    ///   Gets the currently reported parsing exceptions.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    IEnumerable<DataElementException> Exceptions { get; }

    /// <summary>
    ///   Gets the format scheme.
    /// </summary>
    FormatIndicator Format { get; }

    /// <summary>
    ///   Gets the character position of the data element within the barcode data.
    /// </summary>
    int Position { get; }

    /// <summary>
    ///   Gets the index of the record in the barcode. Zero-based.
    /// </summary>
    int Record { get; }

    /// <summary>
    ///   Gets the title of the data element.
    /// </summary>
    string Title { get; }

    /// <summary>
    ///   Adds a parsing exception to the list.
    /// </summary>
    /// <param name="exception">The exception.</param>
    // ReSharper disable once UnusedMemberInSuper.Global
    // ReSharper disable once UnusedMember.Global
    void AddException(DataElementException exception);
}