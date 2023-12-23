// -----------------------------------------------------------------------------------------------------------------
// <copyright file="Callback.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

internal class Callback
{
    internal Callback(int? listenMessageId, Action<NotifyEventArgs> callback)
    {
        Action = callback;
        ListenMessageId = listenMessageId;
    }

    internal Action<NotifyEventArgs> Action { get; }

    internal int? ListenMessageId { get; }
}