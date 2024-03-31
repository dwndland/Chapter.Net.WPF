// -----------------------------------------------------------------------------------------------------------------
// <copyright file="NotifyEventArgs.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF
{
    /// <summary>
    ///     Holds the data passed when a specific WinAPI message has appear. This is used in the <see cref="WindowObserver" />.
    /// </summary>
    public sealed class NotifyEventArgs : EventArgs
    {
        internal NotifyEventArgs(Window observedWindow, int messageId)
        {
            ObservedWindow = observedWindow;
            MessageId = messageId;
        }

        /// <summary>
        ///     Gets the window which has raised the specific WinAPI message.
        /// </summary>
        public Window ObservedWindow { get; }

        /// <summary>
        ///     Gets the appeared WinAPI message.
        /// </summary>
        public int MessageId { get; }
    }
}