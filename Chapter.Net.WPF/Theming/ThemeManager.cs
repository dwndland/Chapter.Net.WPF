// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ThemeManager.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Chapter.Net.WinAPI;
using Chapter.Net.WinAPI.Data;
using Microsoft.Win32;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF
{
    /// <summary>
    ///     Reads or sets the window theme.
    /// </summary>
    public static class ThemeManager
    {
        private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string RegistryValueName = "AppsUseLightTheme";

        /// <summary>
        ///     Sets the theme to use for the given window.
        /// </summary>
        /// <param name="window">The window to modify.</param>
        /// <param name="theme">The theme to use.</param>
        /// <remarks>The window source must be initialized.</remarks>
        /// <returns>True of the theme got applied to the window; otherwise false.</returns>
        public static bool SetWindowTheme(Window window, WindowTheme theme)
        {
            if (theme == WindowTheme.System)
                theme = GetSystemTheme();

            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA.USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                    attribute = DWMWA.USE_IMMERSIVE_DARK_MODE;

                var useDarkMode = theme == WindowTheme.Dark ? 1 : 0;
                var handle = new WindowInteropHelper(window).Handle;
                return Dwmapi.DwmSetWindowAttribute(handle, (int)attribute, ref useDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        /// <summary>
        ///     Gets the theme the system is configured to.
        /// </summary>
        /// <returns>WindowTheme.Light or WindowTheme.Dark depending on the system configuration.</returns>
        public static WindowTheme GetSystemTheme()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                var registryValueObject = key?.GetValue(RegistryValueName);
                if (registryValueObject == null)
                    return WindowTheme.Light;
                var registryValue = (int)registryValueObject;

                return registryValue > 0 ? WindowTheme.Light : WindowTheme.Dark;
            }
        }

        /// <summary>
        ///     Gets the windows accent color.
        /// </summary>
        /// <param name="accent">The concrete accent to get.</param>
        /// <returns>The configured accent color.</returns>
        public static Color GetAccentColor(Accent accent)
        {
            var marshalledName = Marshal.StringToHGlobalUni("Immersive" + accent);
            var colorType = UxTheme.GetImmersiveColorTypeFromName(marshalledName);
            Marshal.FreeHGlobal(marshalledName);
            var nativeColor = UxTheme.GetImmersiveColorFromColorSetEx(0, colorType, false, 0);
            return Color.FromArgb(
                (byte)((0xFF000000 & nativeColor) >> 24),
                (byte)((0x000000FF & nativeColor) >> 0),
                (byte)((0x0000FF00 & nativeColor) >> 8),
                (byte)((0x00FF0000 & nativeColor) >> 16));
        }

        /// <summary>
        ///     Creates the foreground ready to use on the given background.
        /// </summary>
        /// <param name="backgroundColor">The used background color.</param>
        /// <returns>The foreground ready to use on the given background.</returns>
        public static Color GetForegroundByBackground(Color backgroundColor)
        {
            return IsBrightColor(backgroundColor) ? Colors.Black : Colors.White;
        }

        /// <summary>
        ///     Checks if the given color is a bright color.
        /// </summary>
        /// <param name="color">The color to check.</param>
        /// <returns>True if it is a bright color; otherwise false.</returns>
        public static bool IsBrightColor(Color color)
        {
            byte limit = 0x7F;
            return color.A < limit ||
                   (color.R > limit && color.G > limit) ||
                   (color.R > limit && color.B > limit) ||
                   (color.G > limit && color.B > limit);
        }

        private static bool IsWindows10OrGreater(int build)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }
}