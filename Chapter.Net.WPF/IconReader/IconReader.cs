// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IconReader.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Provides a way to read file, extension and folder icons with and without caching.
/// </summary>
public static class IconReader
{
    private static readonly Dictionary<string, ImageSource> _smallFileIcons = new();
    private static readonly Dictionary<string, ImageSource> _largeFileIcons = new();
    private static readonly Dictionary<string, ImageSource> _smallExtensionIcons = new();
    private static readonly Dictionary<string, ImageSource> _largeExtensionIcons = new();
    private static ImageSource _smallFolderIcon;
    private static ImageSource _largeFolderIcon;
    private static readonly Dictionary<string, ImageSource> _smallDriveIcons = new();
    private static readonly Dictionary<string, ImageSource> _largeDriveIcons = new();

    /// <summary>
    ///     Reads the icon of a file. E.g. "C:\Folder\My File.pdf"
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="large">The indicator if the large or the small icon shall be read.</param>
    /// <param name="useCache">The indicator if it shall be stored and read from a cache.</param>
    /// <returns>The icon of the file.</returns>
    public static ImageSource FileIcon(string filePath, bool large, bool useCache)
    {
        var cache = large ? _largeFileIcons : _smallFileIcons;
        if (useCache && cache.TryGetValue(filePath, out var fileIcon))
            return fileIcon;

        var icon = FetchFileIcon(filePath, large);
        if (useCache)
            cache[filePath] = icon;

        return icon;
    }

    /// <summary>
    ///     Reads the icon of a file type using the extension. E.g. ".pdf"
    /// </summary>
    /// <param name="extension">The file type extension.</param>
    /// <param name="large">The indicator if the large or the small icon shall be read.</param>
    /// <param name="useCache">The indicator if it shall be stored and read from a cache.</param>
    /// <returns>The icon of the file type.</returns>
    public static ImageSource ExtensionIcon(string extension, bool large, bool useCache)
    {
        if (!extension.StartsWith("."))
            extension = "." + extension;

        var cache = large ? _largeExtensionIcons : _smallExtensionIcons;
        if (useCache && cache.TryGetValue(extension, out var extensionIcon))
            return extensionIcon;

        var icon = FetchFileIcon(extension, large);
        if (useCache)
            cache[extension] = icon;

        return icon;
    }

    /// <summary>
    ///     Reads the icon of a usual folder. E.g. "C:\Folder"
    /// </summary>
    /// <param name="directory">A directory to read the icon from.</param>
    /// <param name="large">The indicator if the large or the small icon shall be read.</param>
    /// <param name="useCache">The indicator if it shall be stored and read from a cache.</param>
    /// <returns>The icon of the directory.</returns>
    public static ImageSource FolderIcon(string directory, bool large, bool useCache)
    {
        var cache = large ? _largeFolderIcon : _smallFolderIcon;
        if (useCache && cache != null)
            return cache;

        var icon = FetchFolderIcon(directory, large);
        if (useCache)
        {
            if (large)
                _largeFolderIcon = icon;
            else
                _smallFolderIcon = icon;
        }

        return icon;
    }

    /// <summary>
    ///     Reads the icon of a drive. E.g. "C:" or "C:\"
    /// </summary>
    /// <param name="drive">The drive letter.</param>
    /// <param name="large">The indicator if the large or the small icon shall be read.</param>
    /// <param name="useCache">The indicator if it shall be stored and read from a cache.</param>
    /// <returns>The drive icon.</returns>
    public static ImageSource DriveIcon(string drive, bool large, bool useCache)
    {
        var cache = large ? _largeDriveIcons : _smallDriveIcons;
        if (useCache && cache.TryGetValue(drive, out var driveIcon))
            return driveIcon;

        var icon = FetchFolderIcon(drive, large);
        if (useCache)
            cache[drive] = icon;

        return icon;
    }

    private static ImageSource FetchFolderIcon(string name, bool large)
    {
        var icon = FetchIcon(SHGFI.ICON, name, large);
        return Convert(icon);
    }

    private static ImageSource FetchFileIcon(string name, bool large)
    {
        var icon = FetchIcon(SHGFI.ICON | SHGFI.USEFILEATTRIBUTE, name, large);
        return Convert(icon);
    }

    private static Icon FetchIcon(uint flags, string name, bool large)
    {
        flags += large ? SHGFI.LARGEICON : SHGFI.SMALLICON;
        var fileInfo = new SHFILEINFO();
        Shell32.SHGetFileInfo(name, SHGFI.FileAttributeNormal, ref fileInfo, (uint)Marshal.SizeOf(fileInfo), flags);
        var icon = (Icon)Icon.FromHandle(fileInfo.hIcon).Clone();
        User32.DestroyIcon(fileInfo.hIcon);
        return icon;
    }

    private static ImageSource Convert(Icon icon)
    {
        var bmpSrc = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        icon.Dispose();
        return bmpSrc;
    }
}