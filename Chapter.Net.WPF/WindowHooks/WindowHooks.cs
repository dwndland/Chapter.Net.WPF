// -----------------------------------------------------------------------------------------------------------------
// <copyright file="WindowHooks.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Chapter.Net.WinAPI.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Provides a callback to native windows events.
/// </summary>
public class WindowHooks : IWindowHooks
{
    private readonly Proc _proc;
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
        _hookId = SetWindowsHookEx((int)hookType, _proc, GetModuleHandle(module?.ModuleName), 0);
    }

    /// <summary>
    ///     Removes the hook.
    /// </summary>
    public void HookOut()
    {
        if (_hookId == IntPtr.Zero)
            return;

        UnhookWindowsHookEx(_hookId);
        _hookId = IntPtr.Zero;
    }

    private IntPtr HookCallback(int code, IntPtr wParam, IntPtr lParam)
    {
        if (code < 0)
            return CallNextHookEx(_hookId, code, wParam, lParam);

        _callback(code, wParam, lParam);

        return CallNextHookEx(_hookId, code, wParam, lParam);
    }

    [DllImport("user32.dll")]
    private static extern IntPtr SetWindowsHookEx(int hookId, Proc callbackFunction, IntPtr moduleHandle, uint threadId);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnhookWindowsHookEx(IntPtr hookId);

    [DllImport("user32.dll")]
    private static extern IntPtr CallNextHookEx(IntPtr hookId, int code, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetModuleHandle(string moduleName);

    private delegate IntPtr Proc(int code, IntPtr wParam, IntPtr lParam);
}