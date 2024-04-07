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
        internal NotifyEventArgs(Window observedWindow, int messageId, IntPtr wParam, IntPtr lParam)
        {
            ObservedWindow = observedWindow;
            MessageId = messageId;
            WParam = wParam;
            LParam = lParam;
        }

        /// <summary>
        ///     Gets the window which has raised the specific WinAPI message.
        /// </summary>
        public Window ObservedWindow { get; }

        /// <summary>
        ///     Gets the appeared WinAPI message.
        /// </summary>
        public int MessageId { get; }

        /// <summary>
        ///     Gets the wParam of the appeared WinAPI message.
        /// </summary>
        public IntPtr WParam { get; }

        /// <summary>
        ///     Gets the lParam of the appeared WinAPI message.
        /// </summary>
        public IntPtr LParam { get; }
    }
}