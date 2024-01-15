// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataElementException.cs" company="Solidsoft Reply Ltd.">
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
// A data element exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

using System;
using System.Runtime.Serialization;

/// <summary>
///   A data element exception.
/// </summary>
[Serializable]
public class DataElementException : BarcodeException
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="DataElementException" /> class.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public DataElementException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DataElementException" /> class.
    /// </summary>
    /// <param name="message">
    ///   The exception message.
    /// </param>
    // ReSharper disable once UnusedMember.Global
    public DataElementException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DataElementException" /> class.
    /// </summary>
    /// <param name="message">
    ///   The exception message.
    /// </param>
    /// <param name="innerException">
    ///   The inner exception.
    /// </param>
    // ReSharper disable once UnusedMember.Global
    public DataElementException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DataElementException" /> class.
    /// </summary>
    /// <param name="errorNumber">
    ///   The error number.
    /// </param>
    /// <param name="message">
    ///   The message.
    /// </param>
    /// <param name="title">
    ///   The title of the data element.
    /// </param>
    /// <param name="characterPosition">
    ///   The character position in the barcode data at which the exception occurred.
    /// </param>
    /// <param name="isFatal">
    ///   Indicates whether the exception is fatal.
    /// </param>
    public DataElementException(int errorNumber, string message, string title, int characterPosition, bool isFatal)
        : base(errorNumber, message, isFatal)
    {
        Title = title;
        CharacterPosition = characterPosition;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="DataElementException" /> class.
    /// </summary>
    /// <param name="info">The serialization information.</param>
    /// <param name="context">The streaming context.</param>
    [Obsolete("Formatter serialisation has been deprecated in .NET.", DiagnosticId = "SYSLIB0051")]
    protected DataElementException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    ///   Gets the character position in the barcode data where the error occurred.
    /// </summary>
    public int CharacterPosition { get; }

    /// <summary>
    ///   Gets the title of the data element.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string Title { get; } = string.Empty;
}