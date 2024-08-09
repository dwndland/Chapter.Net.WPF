// -----------------------------------------------------------------------------------------------------------------
// <copyright file="Input.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     The base class for the mouse or keyboard inputs used in the <see cref="IInputWatcher" />.
/// </summary>
public abstract class Input
{
    internal abstract void Handle(WH hookType, IntPtr wParam, IntPtr lParam);
}