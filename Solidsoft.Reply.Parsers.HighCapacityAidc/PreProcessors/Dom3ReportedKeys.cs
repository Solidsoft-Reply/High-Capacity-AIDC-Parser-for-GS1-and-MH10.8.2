﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dom3ReportedKeys.cs" company="Solidsoft Reply Ltd.">
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
// Reported DOM3 key events.
// </summary>
// -------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.PreProcessors;

/// <summary>
///   Reported DOM3 key events.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class Dom3ReportedKeys
{
    /// <summary>
    ///   Gets the reported KeyboardEvent code value.
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    ///   Gets the modifiers for the reported code value.
    /// </summary>
    public int Modifiers { get; set; } = 0;
}