// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IWindowHooks.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using Chapter.Net.WinAPI.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Provides a callback to native windows events.
/// </summary>
public interface IWindowHooks
{
    /// <summary>
    ///     Hooks a callback into the window event message queue.
    /// </summary>
    /// <param name="process">The process what main module to use.</param>
    /// <param name="hookType">The type of hooks to listen for.</param>
    /// <param name="callback">The callback executed if a windows message event arrives.</param>
    void HookIn(Process process, WH hookType, Action<int, IntPtr, IntPtr> callback);

    /// <summary>
    ///     Removes the hook.
    /// </summary>
    void HookOut();
}