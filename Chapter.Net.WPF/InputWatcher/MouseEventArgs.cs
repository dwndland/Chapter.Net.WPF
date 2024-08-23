// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MouseEventArgs.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     The event parameter after mouse events got received.
/// </summary>
public class MouseEventArgs
{
    internal MouseEventArgs(ModifierKeys modifiers)
    {
        Modifiers = modifiers;
    }

    /// <summary>
    ///     Gets the pressed modifiers.
    /// </summary>
    public ModifierKeys Modifiers { get; }
}