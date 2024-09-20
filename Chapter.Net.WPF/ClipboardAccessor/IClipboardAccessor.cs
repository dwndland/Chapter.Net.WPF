// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IClipboardAccessor.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows.Media.Imaging;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Wraps access to the windows clipboard.
/// </summary>
public interface IClipboardAccessor
{
    /// <summary>
    ///     Checks if the clipboard contains audio.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains audio; otherwise false.</returns>
    bool ContainsAudio(int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Checks if the clipboard contains data by format.
    /// </summary>
    /// <param name="format">The data format to check for.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains data by format; otherwise false.</returns>
    bool ContainsData(string format, int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Checks if the clipboard contains a file drop list.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains a file drop list; otherwise false.</returns>
    bool ContainsFileDropList(int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Checks if the clipboard contains an image.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains an image; otherwise false.</returns>
    bool ContainsImage(int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Checks if the clipboard contains text.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains text; otherwise false.</returns>
    bool ContainsText(int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Tries to clear the clipboard.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard got cleared; otherwise false.</returns>
    bool TryClear(int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Reads the current clipboard audio stream.
    /// </summary>
    /// <param name="stream">The read audio stream from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard audio stream got accessed; otherwise false.</returns>
    bool TryGetAudioStream(out Stream stream, int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Reads the current clipboard data by format.
    /// </summary>
    /// <param name="format">The data format to read out.</param>
    /// <param name="data">The read data from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard data got accessed; otherwise false.</returns>
    bool TryGetData(string format, out object data, int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Reads the current clipboard file drop list by format.
    /// </summary>
    /// <param name="data">The read file drop list from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard file drop list got accessed; otherwise false.</returns>
    bool TryGetFileDropList(out object data, int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Reads the current clipboard image by format.
    /// </summary>
    /// <param name="bitmapSource">The read image from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard image got accessed; otherwise false.</returns>
    bool TryGetImage(out BitmapSource bitmapSource, int retry = 3, Action<Exception> onError = null);

    /// <summary>
    ///     Reads the current clipboard text.
    /// </summary>
    /// <param name="text">The read text from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard text got accessed; otherwise false.</returns>
    bool TryGetText(out string text, int retry = 3, Action<Exception> onError = null);
}