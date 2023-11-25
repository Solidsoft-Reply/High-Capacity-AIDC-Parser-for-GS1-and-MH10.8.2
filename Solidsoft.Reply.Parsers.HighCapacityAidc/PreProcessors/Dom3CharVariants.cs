// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dom3CharVariants.cs" company="Solidsoft Reply Ltd.">
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
// Character variants for DOM3 keyboard event code processing.
// </summary>
// -------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.PreProcessors;

using System;

/// <summary>
///   Character variants for DOM3 keyboard event code processing.
/// </summary>
public readonly struct Dom3CharVariants : IEquatable<Dom3CharVariants>
{
    /// <summary>
    ///   Initializes a new instance of the <see cref="Dom3CharVariants" /> struct.
    /// </summary>
    /// <param name="character">
    ///   The unmodified character.
    /// </param>
    /// <param name="shift">
    ///   The shifted character.
    /// </param>
    /// <param name="ctrl">
    ///   The control character.
    /// </param>
    /// <param name="shiftCtrl">
    ///   The shift control character.
    /// </param>
    /// <param name="alt">
    ///   The alternative character.
    /// </param>
    /// <param name="shiftAlt">
    ///   The shift alternative character.
    /// </param>
    /// <param name="altGr">
    ///   The alternative graphic character.
    /// </param>
    /// <param name="shiftAltGr">
    ///   The shift alternative graphic character.
    /// </param>
    public Dom3CharVariants(
        int character = 0,
        int shift = 0,
        int ctrl = 0,
        int shiftCtrl = 0,
        int alt = 0,
        int shiftAlt = 0,
        int altGr = 0,
        int shiftAltGr = 0)
    {
        Character = character;
        Shift = shift;
        Ctrl = ctrl;
        ShiftCtrl = shiftCtrl;
        Alt = alt;
        ShiftAlt = shiftAlt;
        AltGr = altGr;
        ShiftAltGr = shiftAltGr;
    }

    /// <summary>
    ///   Gets the unmodified character.
    /// </summary>
    public int Character { get; }

    /// <summary>
    ///   Gets the shifted character.
    /// </summary>
    public int Shift { get; }

    /// <summary>
    ///   Gets the control character.
    /// </summary>
    public int Ctrl { get; }

    /// <summary>
    ///   Gets the shift control character.
    /// </summary>
    public int ShiftCtrl { get; }

    /// <summary>
    ///   Gets the alternative character.
    /// </summary>
    public int Alt { get; }

    /// <summary>
    ///   Gets the shift alternative character.
    /// </summary>
    public int ShiftAlt { get; }

    /// <summary>
    ///   Gets the alternative graphic character.
    /// </summary>
    public int AltGr { get; }

    /// <summary>
    ///   Gets the shift alternative graphic character.
    /// </summary>
    public int ShiftAltGr { get; }

    /// <summary>
    ///   Checks if their operands are equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>true if its operands are equal, false otherwise.</returns>
    public static bool operator ==(Dom3CharVariants left, Dom3CharVariants right)
    {
        return left.Equals(right);
    }

    /// <summary>
    ///   Checks if their operands are not equal.
    /// </summary>
    /// <param name="left">The left operand.</param>
    /// <param name="right">The right operand.</param>
    /// <returns>true if its operands are not equal, false otherwise.</returns>
    public static bool operator !=(Dom3CharVariants left, Dom3CharVariants right)
    {
        return !(left == right);
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is not null && this == (Dom3CharVariants)obj;
    }

    /// <summary>
    ///   Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
    public bool Equals(Dom3CharVariants other)
    {
        return this == other;
    }

    /// <summary>
    ///   Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode() => Character.GetHashCode();
}