// -----------------------------------------------------------------------------------------------------------------
// <copyright file="KeyConverter.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Text;
using System.Windows.Input;
using Chapter.Net.WinAPI;

namespace Chapter.Net.WPF.Converters;

/// <summary>
///     Converts the <see cref="Key" />.
/// </summary>
public static class KeyConverter
{
    /// <summary>
    ///     Converts the <see cref="Key" /> to char.
    /// </summary>
    /// <param name="key">The key to convert.</param>
    /// <returns>The converted char if possible; otherwise ' '.</returns>
    public static char ToChar(Key key)
    {
        var ch = ' ';
        var virtualKey = KeyInterop.VirtualKeyFromKey(key);
        var keyboardState = new byte[256];
        User32.GetKeyboardState(keyboardState);

        var scanCode = User32.MapVirtualKey((uint)virtualKey, MAPVK.VK_TO_VSC);
        var stringBuilder = new StringBuilder(2);

        var result = User32.ToUnicode((uint)virtualKey, scanCode, keyboardState, stringBuilder, stringBuilder.Capacity, 0);
        switch (result)
        {
            case -1:
                break;
            case 0:
                break;
            default:
            {
                ch = stringBuilder[0];
                break;
            }
        }

        return ch;
    }
}