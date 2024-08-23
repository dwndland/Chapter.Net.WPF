// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SystemTexts.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Text;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Loads translations for the current windows language from the system.
/// </summary>
public static class SystemTexts
{
    public const uint OK_CAPTION = 800;
    public const uint CANCEL_CAPTION = 801;
    public const uint ABORT_CAPTION = 802;
    public const uint RETRY_CAPTION = 803;
    public const uint IGNORE_CAPTION = 804;
    public const uint YES_CAPTION = 805;
    public const uint NO_CAPTION = 806;
    public const uint HELP_CAPTION = 808;
    public const uint TRYAGAIN_CAPTION = 809;
    public const uint CONTINUE_CAPTION = 810;

    /// <summary>
    ///     Loads translations for the current windows language from the system.
    /// </summary>
    /// <param name="id">The text ID.</param>
    /// <returns>The translation of the ID in the current system language.</returns>
    public static string GetString(uint id)
    {
        var sb = new StringBuilder(256);
        var user32 = Kernel32.LoadLibrary(Environment.SystemDirectory + "\\User32.dll");
        User32.LoadString(user32, id, sb, sb.Capacity);
        return sb.ToString();
    }
}