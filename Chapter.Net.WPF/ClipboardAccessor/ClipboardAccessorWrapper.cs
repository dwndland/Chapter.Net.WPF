// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ClipboardAccessorWrapper.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Windows.Media.Imaging;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <inheritdoc />
public class ClipboardAccessorWrapper : IClipboardAccessor
{
    /// <inheritdoc />
    public bool ContainsAudio(int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.ContainsAudio(retry, onError);
    }

    /// <inheritdoc />
    public bool ContainsData(string format, int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.ContainsData(format, retry, onError);
    }

    /// <inheritdoc />
    public bool ContainsFileDropList(int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.ContainsFileDropList(retry, onError);
    }

    /// <inheritdoc />
    public bool ContainsImage(int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.ContainsImage(retry, onError);
    }

    /// <inheritdoc />
    public bool ContainsText(int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.ContainsText(retry, onError);
    }

    /// <inheritdoc />
    public bool TryClear(int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.TryClear(retry, onError);
    }

    /// <inheritdoc />
    public bool TryGetAudioStream(out Stream stream, int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.TryGetAudioStream(out stream, retry, onError);
    }

    /// <inheritdoc />
    public bool TryGetData(string format, out object data, int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.TryGetData(format, out data, retry, onError);
    }

    /// <inheritdoc />
    public bool TryGetFileDropList(out object data, int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.TryGetFileDropList(out data, retry, onError);
    }

    /// <inheritdoc />
    public bool TryGetImage(out BitmapSource bitmapSource, int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.TryGetImage(out bitmapSource, retry, onError);
    }

    /// <inheritdoc />
    public bool TryGetText(out string text, int retry = 3, Action<Exception> onError = null)
    {
        return ClipboardAccessor.TryGetText(out text, retry, onError);
    }
}