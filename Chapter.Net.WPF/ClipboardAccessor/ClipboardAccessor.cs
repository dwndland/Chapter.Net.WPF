// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ClipboardAccessor.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Wraps access to the windows clipboard.
/// </summary>
/// <remarks>
///     The System.Windows.Clipboard sometimes crashes with System.Runtime.InteropServices.COMException.
///     The System.Windows.Forms.Clipboard seem to be more stable.
///     https://stackoverflow.com/questions/12769264/openclipboard-failed-when-copy-pasting-data-from-wpf-datagrid
///     In case of a COMException we retry, if it still fails or fails for another reason, we block usage of the clipboard
///     at all.
/// </remarks>
public static class ClipboardAccessor
{
    /// <summary>
    ///     Checks if the clipboard contains audio.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains audio; otherwise false.</returns>
    public static bool ContainsAudio(int retry = 3, Action<Exception> onError = null)
    {
        return Check(Clipboard.ContainsAudio, retry, onError);
    }

    /// <summary>
    ///     Checks if the clipboard contains data by format.
    /// </summary>
    /// <param name="format">The data format to check for.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains data by format; otherwise false.</returns>
    public static bool ContainsData(string format, int retry = 3, Action<Exception> onError = null)
    {
        return Check(() => Clipboard.ContainsData(format), retry, onError);
    }

    /// <summary>
    ///     Checks if the clipboard contains a file drop list.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains a file drop list; otherwise false.</returns>
    public static bool ContainsFileDropList(int retry = 3, Action<Exception> onError = null)
    {
        return Check(Clipboard.ContainsFileDropList, retry, onError);
    }

    /// <summary>
    ///     Checks if the clipboard contains an image.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains an image; otherwise false.</returns>
    public static bool ContainsImage(int retry = 3, Action<Exception> onError = null)
    {
        return Check(Clipboard.ContainsImage, retry, onError);
    }

    /// <summary>
    ///     Checks if the clipboard contains text.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard can be accessed and contains text; otherwise false.</returns>
    public static bool ContainsText(int retry = 3, Action<Exception> onError = null)
    {
        return Check(Clipboard.ContainsText, retry, onError);
    }

    /// <summary>
    ///     Tries to clear the clipboard.
    /// </summary>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard got cleared; otherwise false.</returns>
    public static bool TryClear(int retry = 3, Action<Exception> onError = null)
    {
        var result = Get(() =>
            {
                Clipboard.Clear();
                return true;
            },
            retry,
            onError);
        return result.Item1;
    }

    /// <summary>
    ///     Reads the current clipboard audio stream.
    /// </summary>
    /// <param name="stream">The read audio stream from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard audio stream got accessed; otherwise false.</returns>
    public static bool TryGetAudioStream(out Stream stream, int retry = 3, Action<Exception> onError = null)
    {
        var result = Get(Clipboard.GetAudioStream, retry, onError);
        stream = result.Item2;
        return result.Item1;
    }

    /// <summary>
    ///     Reads the current clipboard data by format.
    /// </summary>
    /// <param name="format">The data format to read out.</param>
    /// <param name="data">The read data from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard data got accessed; otherwise false.</returns>
    public static bool TryGetData(string format, out object data, int retry = 3, Action<Exception> onError = null)
    {
        var result = Get(() => Clipboard.GetData(format), retry, onError);
        data = result.Item2;
        return result.Item1;
    }

    /// <summary>
    ///     Reads the current clipboard file drop list by format.
    /// </summary>
    /// <param name="data">The read file drop list from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard file drop list got accessed; otherwise false.</returns>
    public static bool TryGetFileDropList(out object data, int retry = 3, Action<Exception> onError = null)
    {
        var result = Get(Clipboard.GetFileDropList, retry, onError);
        data = result.Item2;
        return result.Item1;
    }

    /// <summary>
    ///     Reads the current clipboard image by format.
    /// </summary>
    /// <param name="bitmapSource">The read image from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard image got accessed; otherwise false.</returns>
    public static bool TryGetImage(out BitmapSource bitmapSource, int retry = 3, Action<Exception> onError = null)
    {
        var result = Get(() => ConvertImage(Clipboard.GetImage()), retry, onError);
        bitmapSource = result.Item2;
        return result.Item1;
    }

    /// <summary>
    ///     Reads the current clipboard text.
    /// </summary>
    /// <param name="text">The read text from the clipboard if accessible.</param>
    /// <param name="retry">The amount of retry if a COMException was raised. For other exception its unused.</param>
    /// <param name="onError">Callback for any error. It will be called also for each single retry.</param>
    /// <returns>True if the clipboard text got accessed; otherwise false.</returns>
    public static bool TryGetText(out string text, int retry = 3, Action<Exception> onError = null)
    {
        var result = Get(Clipboard.GetText, retry, onError);
        text = result.Item2;
        return result.Item1;
    }

    private static bool Check(Func<bool> check, int retry = 3, Action<Exception> onError = null)
    {
        for (var i = 0; i < retry; ++i)
            try
            {
                return check();
            }
            catch (COMException ex)
            {
                onError?.Invoke(ex);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                break;
            }

        return false;
    }

    private static (bool, T) Get<T>(Func<T> get, int retry = 3, Action<Exception> onError = null)
    {
        for (var i = 0; i < retry; ++i)
            try
            {
                return (true, get());
            }
            catch (COMException ex)
            {
                onError?.Invoke(ex);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                break;
            }

        return (false, default);
    }

    private static BitmapImage ConvertImage(Image image)
    {
        using var memoryStream = new MemoryStream();
        image.Save(memoryStream, ImageFormat.Png);
        memoryStream.Position = 0;
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = memoryStream;
        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        bitmapImage.EndInit();
        bitmapImage.Freeze();
        return bitmapImage;
    }
}