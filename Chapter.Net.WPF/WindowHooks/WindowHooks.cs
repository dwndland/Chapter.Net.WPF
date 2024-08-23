// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WindowHooks.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Provides a callback to native windows events.
/// </summary>
public class WindowHooks : IWindowHooks
{
    private readonly User32.Proc _proc;
    private Action<int, IntPtr, IntPtr> _callback;
    private IntPtr _hookId;

    /// <summary>
    ///     Creates a new WindowHooks.
    /// </summary>
    public WindowHooks()
    {
        _proc = HookCallback; // Unmanaged callbacks has to be kept alive
        _hookId = IntPtr.Zero;
    }

    /// <summary>
    ///     Hooks a callback into the window event message queue.
    /// </summary>
    /// <param name="process">The process what main module to use.</param>
    /// <param name="hookType">The type of hooks to listen for.</param>
    /// <param name="callback">The callback executed if a windows message event arrives.</param>
    public void HookIn(Process process, WH hookType, Action<int, IntPtr, IntPtr> callback)
    {
        _callback = callback;

        if (_hookId != IntPtr.Zero)
            return;

        using var module = process.MainModule;
        _hookId = User32.SetWindowsHookEx((int)hookType, _proc, Kernel32.GetModuleHandle(module?.ModuleName), 0);
    }

    /// <summary>
    ///     Removes the hook.
    /// </summary>
    public void HookOut()
    {
        if (_hookId == IntPtr.Zero)
            return;

        User32.UnhookWindowsHookEx(_hookId);
        _hookId = IntPtr.Zero;
    }

    private IntPtr HookCallback(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code < 0)
            return User32.CallNextHookEx(_hookId, code, wParam, lParam);

        _callback(code, wParam, lParam);

        return User32.CallNextHookEx(_hookId, code, wParam, lParam);
    }
}