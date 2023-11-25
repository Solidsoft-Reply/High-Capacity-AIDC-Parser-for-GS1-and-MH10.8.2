// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Dom3KeyboardEventCodes.cs" company="Solidsoft Reply Ltd.">
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
// Pre-processor methods for DOM3 KeyboardEvent Code property values.
// </summary>
// -------------------------------------------------------------------------------------------------------------------

namespace Solidsoft.Reply.Parsers.HighCapacityAidc.PreProcessors;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Newtonsoft.Json;

/// <summary>
///   Pre-processor methods for DOM3 KeyboardEvent Code property values.
/// </summary>
// ReSharper disable once UnusedMember.Global
// ReSharper disable once UnusedType.Global
public static class Dom3KeyboardEventCodes
{
    /// <summary>
    ///   A dictionary of mappings from textual codes to numeric code values.
    /// </summary>
    private static readonly Dictionary<string, Dom3CharVariants> CodeValueMap =
        new()
        {
            { "Backquote", new Dom3CharVariants(0x0060, 0x007E, 0x00E0, 0x00F0, 0x01E0, 0x02F0, 0x03E0, 0x04F0) },
            { "Backslash", new Dom3CharVariants(0x005C, 0x007C, 0x001C, 0x001C, 0x011C, 0x021C, 0x031C, 0x041C) },
            { "Backspace", new Dom3CharVariants(0x0008, 0x0008, 0x0008, 0x0008, 0x0008, 0x0008, 0x0008, 0x0408) },
            { "BracketLeft", new Dom3CharVariants(0x005B, 0x007B, 0x001B, 0x001B, 0x011B, 0x021B, 0x031B, 0x041B) },
            {
                "BracketRight", new Dom3CharVariants(0x005D, 0x007D, 0x001D, 0x001D, 0x011D, 0x021D, 0x031D, 0x041D)
            },
            { "Comma", new Dom3CharVariants(0x002C, 0x003C, 0x00AC, 0x00BC, 0x01AC, 0x02BC, 0x03AC, 0x04BC) },
            { "Digit0", new Dom3CharVariants(0x0030, 0x0029, 0x00B0, 0x00C0, 0x01B0, 0x02C0, 0x03B0, 0x04C0) },
            { "Digit1", new Dom3CharVariants(0x0031, 0x0021, 0x00B1, 0x00C1, 0x01B1, 0x02C1, 0x03B1, 0x04C1) },
            { "Digit2", new Dom3CharVariants(0x0032, 0x0040, 0x00B2, 0x00C2, 0x01B2, 0x02C2, 0x03B2, 0x04C2) },
            { "Digit3", new Dom3CharVariants(0x0033, 0x0023, 0x00B3, 0x00C3, 0x01B3, 0x02C3, 0x03B3, 0x04C3) },
            { "Digit4", new Dom3CharVariants(0x0034, 0x0024, 0x00B4, 0x00C4, 0x01B4, 0x02C4, 0x03B4, 0x04C4) },
            { "Digit5", new Dom3CharVariants(0x0035, 0x0025, 0x00B5, 0x00C5, 0x01B5, 0x02C5, 0x03B5, 0x04C5) },
            { "Digit6", new Dom3CharVariants(0x0036, 0x005E, 0x001E, 0x001E, 0x011E, 0x021E, 0x031E, 0x041E) },
            { "Digit7", new Dom3CharVariants(0x0037, 0x0026, 0x00B7, 0x00C7, 0x01B7, 0x02C7, 0x03B7, 0x04C7) },
            { "Digit8", new Dom3CharVariants(0x0038, 0x002A, 0x00B8, 0x00C8, 0x01B8, 0x02C8, 0x03B8, 0x04C8) },
            { "Digit9", new Dom3CharVariants(0x0039, 0x0028, 0x00B9, 0x00C9, 0x01B9, 0x02C9, 0x03B9, 0x04C9) },
            { "Equal", new Dom3CharVariants(0x003D, 0x002B, 0x00BD, 0x00CD, 0x01BD, 0x02CD, 0x03BD, 0x04CD) },
            {
                // ReSharper disable once StringLiteralTypo
                "IntlBackslash", new Dom3CharVariants(0x005C, 0x007C, 0x00DC, 0x00EC, 0x01DC, 0x02EC, 0x03DC, 0x04EC)
            },
            {
                // ReSharper disable once StringLiteralTypo
                "IntlHash", new Dom3CharVariants(0x0023, 0x007E, 0x00A3, 0x00B4, 0x01A3, 0x02B4, 0x03A3, 0x04B4)
            },
            // ReSharper disable once StringLiteralTypo
            { "IntlRo", new Dom3CharVariants(0x005C, 0x308D, 0x00DC, 0x00EC, 0x01DC, 0x02EC, 0x03DC, 0x04EC) },

            // ReSharper disable once StringLiteralTypo
            { "IntlYen", new Dom3CharVariants(0x00A5, 0x002F, 0x0125, 0x0135, 0x0126, 0x0236, 0x0326, 0x0436) },
            { "KeyA", new Dom3CharVariants(0x0061, 0x0041, 0x0001, 0x0001, 0x0101, 0x0201, 0x0301, 0x0401) },
            { "KeyB", new Dom3CharVariants(0x0062, 0x0042, 0x0002, 0x0002, 0x0102, 0x0202, 0x0302, 0x0402) },
            { "KeyC", new Dom3CharVariants(0x0063, 0x0043, 0x0003, 0x0003, 0x0103, 0x0203, 0x0303, 0x0403) },
            { "KeyD", new Dom3CharVariants(0x0064, 0x0044, 0x0004, 0x0004, 0x0104, 0x0204, 0x0304, 0x0404) },
            { "KeyE", new Dom3CharVariants(0x0065, 0x0045, 0x0005, 0x0005, 0x0105, 0x0205, 0x0305, 0x0405) },
            { "KeyF", new Dom3CharVariants(0x0066, 0x0046, 0x0006, 0x0006, 0x0106, 0x0206, 0x0306, 0x0406) },
            { "KeyG", new Dom3CharVariants(0x0067, 0x0047, 0x0007, 0x0007, 0x0107, 0x0207, 0x0307, 0x0407) },
            { "KeyH", new Dom3CharVariants(0x0068, 0x0048, 0x0008, 0x0008, 0x0108, 0x0208, 0x0308, 0x0408) },
            { "KeyI", new Dom3CharVariants(0x0069, 0x0049, 0x0009, 0x0009, 0x0109, 0x0209, 0x0309, 0x0409) },
            { "KeyJ", new Dom3CharVariants(0x006A, 0x004A, 0x000A, 0x000A, 0x010A, 0x020A, 0x030A, 0x040A) },
            { "KeyK", new Dom3CharVariants(0x006B, 0x004B, 0x000B, 0x000B, 0x010B, 0x020B, 0x030B, 0x040B) },
            { "KeyL", new Dom3CharVariants(0x006C, 0x004C, 0x000C, 0x000C, 0x010C, 0x020C, 0x030C, 0x040C) },
            { "KeyM", new Dom3CharVariants(0x006D, 0x004D, 0x000D, 0x000D, 0x010D, 0x020D, 0x030D, 0x040D) },
            { "KeyN", new Dom3CharVariants(0x006E, 0x004E, 0x000E, 0x000E, 0x010E, 0x020E, 0x030E, 0x040E) },
            { "KeyO", new Dom3CharVariants(0x006F, 0x004F, 0x000F, 0x000F, 0x010F, 0x020F, 0x030F, 0x040F) },
            { "KeyP", new Dom3CharVariants(0x0070, 0x0050, 0x0010, 0x0010, 0x0110, 0x0210, 0x0310, 0x0410) },
            { "KeyQ", new Dom3CharVariants(0x0071, 0x0051, 0x0011, 0x0011, 0x0111, 0x0211, 0x0311, 0x0411) },
            { "KeyR", new Dom3CharVariants(0x0072, 0x0052, 0x0012, 0x0012, 0x0112, 0x0212, 0x0312, 0x0412) },
            { "KeyS", new Dom3CharVariants(0x0073, 0x0053, 0x0013, 0x0013, 0x0113, 0x0213, 0x0313, 0x0413) },
            { "KeyT", new Dom3CharVariants(0x0074, 0x0054, 0x0014, 0x0014, 0x0114, 0x0214, 0x0314, 0x0414) },
            { "KeyU", new Dom3CharVariants(0x0075, 0x0055, 0x0015, 0x0015, 0x0115, 0x0215, 0x0315, 0x0415) },
            { "KeyV", new Dom3CharVariants(0x0076, 0x0056, 0x0016, 0x0016, 0x0116, 0x0216, 0x0316, 0x0416) },
            { "KeyW", new Dom3CharVariants(0x0077, 0x0057, 0x0017, 0x0017, 0x0117, 0x0217, 0x0317, 0x0417) },
            { "KeyX", new Dom3CharVariants(0x0078, 0x0058, 0x0018, 0x0018, 0x0118, 0x0218, 0x0318, 0x0418) },
            { "KeyY", new Dom3CharVariants(0x0079, 0x0059, 0x0019, 0x0019, 0x0119, 0x0219, 0x0319, 0x0419) },
            { "KeyZ", new Dom3CharVariants(0x007A, 0x005A, 0x001A, 0x001A, 0x011A, 0x021A, 0x031A, 0x041A) },
            { "Minus", new Dom3CharVariants(0x002D, 0x005F, 0x001F, 0x001F, 0x011F, 0x021F, 0x031F, 0x041F) },
            { "Period", new Dom3CharVariants(0x002E, 0x003E, 0x00AE, 0x00BE, 0x01AE, 0x02BE, 0x03AE, 0x04BE) },
            { "Quote", new Dom3CharVariants(0x0027, 0x0022, 0x00A7, 0x00B8, 0x01A7, 0x02B8, 0x03A7, 0x04B8) },
            { "Semicolon", new Dom3CharVariants(0x003B, 0x003A, 0x00BB, 0x00CB, 0x01BB, 0x02CB, 0x03BB, 0x04CB) },
            { "Slash", new Dom3CharVariants(0x002F, 0x003F, 0x00AF, 0x00BF, 0x01AF, 0x02BF, 0x03AF, 0x04BF) },
            { "AltLeft", new Dom3CharVariants() },
            { "AltRight", new Dom3CharVariants() },
            { "CapsLock", new Dom3CharVariants() },
            { "ContextMenu", new Dom3CharVariants() },
            { "ControlLeft", new Dom3CharVariants() },
            { "ControlRight", new Dom3CharVariants() },
            { "Enter", new Dom3CharVariants(0x000D) },
            { "MetaLeft", new Dom3CharVariants() },
            { "OSLeft", new Dom3CharVariants() },
            { "MetaRight", new Dom3CharVariants() },
            { "OSRight", new Dom3CharVariants() },
            { "ShiftLeft", new Dom3CharVariants() },
            { "ShiftRight", new Dom3CharVariants() },
            { "Space", new Dom3CharVariants(0x0020) },
            { "Tab", new Dom3CharVariants(0x0009) },
            { "Convert", new Dom3CharVariants() },
            { "KanaMode", new Dom3CharVariants() },
            { "Lang1", new Dom3CharVariants() },
            { "Lang2", new Dom3CharVariants() },
            { "RomanCharacters", new Dom3CharVariants() },
            { "Lang3", new Dom3CharVariants() },
            { "Lang4", new Dom3CharVariants() },
            { "Lang5", new Dom3CharVariants() },
            { "NonConvert", new Dom3CharVariants() },
            { "Delete", new Dom3CharVariants(0x007F) },
            { "End", new Dom3CharVariants() },
            { "Help", new Dom3CharVariants() },
            { "Home", new Dom3CharVariants() },
            { "Insert", new Dom3CharVariants() },
            { "PageDown", new Dom3CharVariants() },
            { "PageUp", new Dom3CharVariants() },
            { "ArrowDown", new Dom3CharVariants() },
            { "ArrowLeft", new Dom3CharVariants() },
            { "ArrowRight", new Dom3CharVariants() },
            { "ArrowUp", new Dom3CharVariants() },
            { "NumLock", new Dom3CharVariants() },
            { "Numpad0", new Dom3CharVariants(0x0030) },
            { "Numpad1", new Dom3CharVariants(0x0031) },
            { "Numpad2", new Dom3CharVariants(0x0032) },
            { "Numpad3", new Dom3CharVariants(0x0033) },
            { "Numpad4", new Dom3CharVariants(0x0034) },
            { "Numpad5", new Dom3CharVariants(0x0035) },
            { "Numpad6", new Dom3CharVariants(0x0036) },
            { "Numpad7", new Dom3CharVariants(0x0037) },
            { "Numpad8", new Dom3CharVariants(0x0038) },
            { "Numpad9", new Dom3CharVariants(0x0039) },
            { "NumpadAdd", new Dom3CharVariants(0x002B) },
            { "NumpadBackspace", new Dom3CharVariants() },
            { "NumpadClear", new Dom3CharVariants() },
            { "NumpadClearEntry", new Dom3CharVariants() },
            { "NumpadComma", new Dom3CharVariants(0x002C) },
            { "NumpadDecimal", new Dom3CharVariants(0x002E) },
            { "NumpadDivide", new Dom3CharVariants(0x002F) },
            { "NumpadEnter", new Dom3CharVariants(0x000D) },
            { "NumpadEqual", new Dom3CharVariants(0x003D) },
            { "NumpadHash", new Dom3CharVariants(0x0023) },
            { "NumpadMemoryAdd", new Dom3CharVariants() },
            { "NumpadMemoryClear", new Dom3CharVariants() },
            { "NumpadMemoryRecall", new Dom3CharVariants() },
            { "NumpadMemoryStore", new Dom3CharVariants() },
            { "NumpadMemorySubtract", new Dom3CharVariants() },
            { "NumpadMultiply", new Dom3CharVariants(0x002A) },
            { "NumpadParenLeft", new Dom3CharVariants() },
            { "NumpadParenRight", new Dom3CharVariants() },
            { "NumpadStar", new Dom3CharVariants(0x002A) },
            { "NumpadSubtract", new Dom3CharVariants() },
            { "Escape", new Dom3CharVariants(0x001B) },
            { "F1", new Dom3CharVariants() },
            { "F2", new Dom3CharVariants() },
            { "F3", new Dom3CharVariants() },
            { "F4", new Dom3CharVariants() },
            { "F5", new Dom3CharVariants() },
            { "F6", new Dom3CharVariants() },
            { "F7", new Dom3CharVariants() },
            { "F8", new Dom3CharVariants() },
            { "F9", new Dom3CharVariants() },
            { "F10", new Dom3CharVariants() },
            { "F11", new Dom3CharVariants() },
            { "F12", new Dom3CharVariants() },
            { "Fn", new Dom3CharVariants() },
            { "FnLock", new Dom3CharVariants() },
            { "PrintScreen", new Dom3CharVariants() },
            { "ScrollLock", new Dom3CharVariants() },
            { "Pause", new Dom3CharVariants() },
            { "BrowserBack", new Dom3CharVariants() },
            { "BrowserFavorites", new Dom3CharVariants() },
            { "BrowserForward", new Dom3CharVariants() },
            { "BrowserHome", new Dom3CharVariants() },
            { "BrowserRefresh", new Dom3CharVariants() },
            { "BrowserSearch", new Dom3CharVariants() },
            { "BrowserStop", new Dom3CharVariants() },
            { "Eject", new Dom3CharVariants() },
            { "LaunchApp1", new Dom3CharVariants() },
            { "LaunchApp2", new Dom3CharVariants() },
            { "LaunchMail", new Dom3CharVariants() },
            { "MediaPlayPause", new Dom3CharVariants() },
            { "LaunchMediaPlayer", new Dom3CharVariants() },
            { "MediaSelect", new Dom3CharVariants() },
            { "MediaStop", new Dom3CharVariants() },
            { "MediaTrackNext", new Dom3CharVariants() },
            { "MediaTrackPrevious", new Dom3CharVariants() },
            { "Power", new Dom3CharVariants() },
            { "Sleep", new Dom3CharVariants() },
            { "AudioVolumeDown", new Dom3CharVariants() },
            { "VolumeDown", new Dom3CharVariants() },
            { "AudioVolumeMute", new Dom3CharVariants() },
            { "VolumeMute", new Dom3CharVariants() },
            { "AudioVolumeUp", new Dom3CharVariants() },
            { "VolumeUp", new Dom3CharVariants() },
            { "WakeUp", new Dom3CharVariants() },
            { "Hyper", new Dom3CharVariants() },
            { "Super", new Dom3CharVariants() },
            { "Turbo", new Dom3CharVariants() },
            { "Abort", new Dom3CharVariants() },
            { "Resume", new Dom3CharVariants() },
            { "Suspend", new Dom3CharVariants() },
            { "Again", new Dom3CharVariants() },
            { "Copy", new Dom3CharVariants() },
            { "Cut", new Dom3CharVariants() },
            { "Find", new Dom3CharVariants() },
            { "Open", new Dom3CharVariants() },
            { "Paste", new Dom3CharVariants() },
            { "Props", new Dom3CharVariants() },
            { "Select", new Dom3CharVariants() },
            { "Undo", new Dom3CharVariants() },
            { "Hiragana", new Dom3CharVariants() },
            { "Katakana", new Dom3CharVariants() },
            { "F13", new Dom3CharVariants() },
            { "F14", new Dom3CharVariants() },
            { "F15", new Dom3CharVariants() },
            { "F16", new Dom3CharVariants() },
            { "F17", new Dom3CharVariants() },
            { "F18", new Dom3CharVariants() },
            { "F19", new Dom3CharVariants() },
            { "F20", new Dom3CharVariants() },
            { "F21", new Dom3CharVariants() },
            { "F22", new Dom3CharVariants() },
            { "F23", new Dom3CharVariants() },
            { "F24", new Dom3CharVariants() },
            { "Unidentified", new Dom3CharVariants() }

            // ReSharper disable once StringLiteralTypo
        };

    /// <summary>
    ///   A pre-processor that converts a JSON representation of HTML DOM 3 keyboard codes (scan codes) into literal characters.
    /// </summary>
    /// <param name="input">The barcode data input.</param>
    /// <returns>The pre-processed barcode data input.</returns>
    // ReSharper disable once UnusedMember.Global
    public static string ConvertCodesToString(string input)
    {
        /*  The code expects a JSON list of the following records, where each record
         *  represents a scan code sent to the computer.
         *
         * { "code" : "<code>", "modifiers" : <flags> }
         *
         * ...where the modifiers flag is calculated by summing the following values for
         * each modifier that is current:
         *
         *  Shift: 1
         *  Control: 2
         *  Alt: 4
         *  AltGr: 8
         *  Meta: 16
         *
         * NB. The ALtGr key is not distinguished by DOM3 KeyboardEvent flags and must be
         * inferred in the JavaScript event handlers.
         *
         * e.g.,
         * [
         *   { "code" : "KeyH", "modifiers" : 1 },
         *   { "code" : "KeyE", "modifiers" : 0 },
         *   { "code" : "KeyL", "modifiers" : 0 },
         *   { "code" : "KeyL", "modifiers" : 0 },
         *   { "code" : "KeyO", "modifiers" : 0 },
         *   { "code" : "Space", "modifiers" : 0 },
         *   { "code" : "KeyW", "modifiers" : 0 },
         *   { "code" : "KeyO", "modifiers" : 0 },
         *   { "code" : "KeyR", "modifiers" : 0 },
         *   { "code" : "KeyL", "modifiers" : 0 },
         *   { "code" : "KeyD", "modifiers" : 0 },
         *   { "code" : "Digit1", "modifiers" : 1},
         *   { "code" : "Enter", "modifiers" : 0 },
         * ]
         *  */
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        var processedCharacterValues = new List<int>();
        var reportedKeys = JsonConvert.DeserializeObject<List<Dom3ReportedKeys>>(input);
        var keypadAltCharNumber = new StringBuilder();

        if (reportedKeys is not null)
        {
            foreach (var key in reportedKeys)
            {
                Dom3CharVariants characterVariants;

                try
                {
                    characterVariants = CodeValueMap[key.Code];
                }
                catch (ArgumentNullException)
                {
                    // The code was not found. Record an ASCII 0.
                    processedCharacterValues.Add(0);
                    continue;
                }
                catch (KeyNotFoundException)
                {
                    // The code was not found. Record an ASCII 0.
                    processedCharacterValues.Add(0);
                    continue;
                }

                int characterValue;

                switch (key.Modifiers)
                {
                    case 0:
                        if (key.Code == "AltLeft" && keypadAltCharNumber.Length > 0)
                        {
                            var altCharacterValue = 0;

                            try
                            {
                                altCharacterValue = Convert.ToInt32(
                                    keypadAltCharNumber.ToString(),
                                    CultureInfo.InvariantCulture);
                            }
                            catch (FormatException)
                            {
                                // ignored
                            }
                            catch (OverflowException)
                            {
                                // ignored
                            }

                            processedCharacterValues.Add(altCharacterValue);
                            keypadAltCharNumber.Clear();
                        }

                        characterValue = characterVariants.Character;
                        break;
                    case 1:
                        characterValue = characterVariants.Shift;
                        break;
                    case 2:
                        characterValue = characterVariants.Ctrl;
                        break;
                    case 3:
                        characterValue = characterVariants.ShiftCtrl;
                        break;
                    case 4:
                        // Check if numeric keypad entry is being provided for OEM/ANSI/UNICODE character entry
                        switch (key.Code)
                        {
                            case "Numpad0":
                                keypadAltCharNumber.Append(0);
                                continue;
                            case "Numpad1":
                                keypadAltCharNumber.Append(1);
                                continue;
                            case "Numpad2":
                                keypadAltCharNumber.Append(2);
                                continue;
                            case "Numpad3":
                                keypadAltCharNumber.Append(3);
                                continue;
                            case "Numpad4":
                                keypadAltCharNumber.Append(4);
                                continue;
                            case "Numpad5":
                                keypadAltCharNumber.Append(5);
                                continue;
                            case "Numpad6":
                                keypadAltCharNumber.Append(6);
                                continue;
                            case "Numpad7":
                                keypadAltCharNumber.Append(7);
                                continue;
                            case "Numpad8":
                                keypadAltCharNumber.Append(8);
                                continue;
                            case "Numpad9":
                                keypadAltCharNumber.Append(9);
                                continue;
                        }

                        characterValue = characterVariants.Alt;
                        break;
                    case 5:
                        characterValue = characterVariants.ShiftAlt;
                        break;
                    case 8:
                        characterValue = characterVariants.AltGr;
                        break;
                    case 9:
                        characterValue = characterVariants.ShiftAltGr;
                        break;
                    default:
                        characterValue = 0;
                        break;
                }

                if (keypadAltCharNumber.Length > 0)
                {
                    var altCharacterValue = 0;

                    try
                    {
                        altCharacterValue = Convert.ToInt32(
                            keypadAltCharNumber.ToString(),
                            CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        // ignored
                    }
                    catch (OverflowException)
                    {
                        // ignored
                    }

                    processedCharacterValues.Add(altCharacterValue);
                    keypadAltCharNumber.Clear();
                }

                if (characterValue > 0)
                {
                    processedCharacterValues.Add(characterValue);
                }
            }
        }

        var output = new StringBuilder();

        foreach (var charValue in processedCharacterValues)
        {
            if (output.Length > 0)
            {
                output.Append(',');
            }

            output.Append(charValue.ToString(CultureInfo.InvariantCulture));
        }

        var characters = output.ToString().Split(',');
        var characterOutput = new StringBuilder();

        foreach (var characterValue in characters)
        {
            characterOutput.Append((char)Convert.ToInt32(characterValue, CultureInfo.InvariantCulture));
        }

        return characterOutput.ToString();
    }
}