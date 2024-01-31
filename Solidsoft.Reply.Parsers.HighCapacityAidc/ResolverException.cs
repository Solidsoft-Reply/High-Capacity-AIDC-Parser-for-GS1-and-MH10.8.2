﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolverException.cs" company="Solidsoft Reply Ltd.">
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
// An exception raised at the entity resolution stage.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc;

using System;
using System.Runtime.Serialization;

/// <summary>
///   An exception raised at the entity resolution stage.
/// </summary>
[Serializable]
public class ResolverException : Exception
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="ResolverException" /> class.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public ResolverException()
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResolverException" /> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    // ReSharper disable once UnusedMember.Global
    public ResolverException(string message)
        : base(message)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResolverException" /> class.
    /// </summary>
    /// <param name="message">The exception message.</param>
    /// <param name="innerException">The inner exception.</param>
    // ReSharper disable once UnusedMember.Global
    public ResolverException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResolverException" /> class.
    /// </summary>
    /// <param name="errorNumber">The error number.</param>
    /// <param name="message">The error message.</param>
    /// <param name="isFatal">Indicates whether the exception is fatal. </param>
    /// <param name="offset">The character position of the exception.</param>
    // ReSharper disable once StyleCop.SA1642
    public ResolverException(int errorNumber, string message, bool isFatal, int offset = 0)
        : base(message)
    {
        ErrorNumber = errorNumber;
        IsFatal = isFatal;
        Offset = offset;
    }

    /// <summary>
    ///   Initializes a new instance of the <see cref="ResolverException" /> class.
    /// </summary>
    /// <param name="info">The serialization information.</param>
    /// <param name="context">The streaming context.</param>
#if NET5_0_OR_GREATER
    [Obsolete("Formatter serialisation has been deprecated in .NET.", DiagnosticId = "SYSLIB0051")]
#endif
    protected ResolverException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    /// <summary>
    ///   Gets the error number.
    /// </summary>
    public int ErrorNumber { get; }

    /// <summary>
    ///   Gets a value indicating whether the exception is fatal. Fatality means that the
    ///   parser must abandon the attempt to parse the exception.
    /// </summary>
    public bool IsFatal { get; }

    /// <summary>
    ///   Gets the offset position of the exception.
    /// </summary>
    public int Offset { get; }
}