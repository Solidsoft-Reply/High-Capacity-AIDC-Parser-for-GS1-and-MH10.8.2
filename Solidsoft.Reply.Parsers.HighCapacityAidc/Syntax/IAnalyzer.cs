// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAnalyzer.cs" company="Solidsoft Reply Ltd.">
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
// Represents an analyzer used to determine the syntactic format of each record in the barcode.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.Syntax;

using System.Collections.Generic;

/// <summary>
///   Represents an analyzer used to determine the syntactic format of each record in the barcode.
/// </summary>
public interface IAnalyzer
{
    /// <summary>
    ///   Analyzes the syntactic format of each record in the barcode
    /// </summary>
    /// <param name="data">The data to be analyzed.</param>
    /// <param name="characterPosition">The current character position.</param>
    /// <param name="messageHeader">Indicates if a message header was found.</param>
    /// <returns>A list of formats for each record in the barcode.</returns>
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once UnusedMemberInSuper.Global
    IList<IFormat> Analyze(string data, int characterPosition, out bool messageHeader);
}