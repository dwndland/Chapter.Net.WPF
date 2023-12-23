// -----------------------------------------------------------------------------------------------------------------
// <copyright file="InputWatcher.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Chapter.Net.WinAPI.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <inheritdoc />
public class InputWatcher : IInputWatcher
{
    private readonly Dictionary<SubscribeToken, Input> _callbacks;
    private WindowHooks _windowKeyboardHooks;
    private WindowHooks _windowsMouseHooks;

    /// <summary>
    ///     Creates a new InputWatcher.
    /// </summary>
    public InputWatcher()
    {
        _callbacks = new Dictionary<SubscribeToken, Input>();
    }

    /// <inheritdoc />
    public bool IsObserving { get; private set; }

    /// <inheritdoc />
    public SubscribeToken Observe(Input input)
    {
        var token = new SubscribeToken();
        token.Disposed += OnTokenDisposed;
        _callbacks.Add(token, input);
        SyncHooks();
        return token;
    }

    /// <inheritdoc />
    public void Start()
    {
        IsObserving = true;
        SyncHooks();
    }

    /// <inheritdoc />
    public void Stop()
    {
        IsObserving = false;

        _windowKeyboardHooks?.HookOut();
        _windowKeyboardHooks = null;

        _windowsMouseHooks?.HookOut();
        _windowsMouseHooks = null;
    }

    /// <summary>
    ///     Stops the input watching and frees all callbacks.
    /// </summary>
    public void Dispose()
    {
        Stop();
        _callbacks.Clear();
    }

    private void SyncHooks()
    {
        if (!IsObserving)
            return;

        Process process = null;
        if (_windowKeyboardHooks == null && _callbacks.Any(x => x.Value is KeyboardInput))
        {
            process = Process.GetCurrentProcess();
            _windowKeyboardHooks = new WindowHooks();
            _windowKeyboardHooks.HookIn(process, WH.KEYBOARD_LL, KeyboardEventReceived);
        }
        else if (_windowKeyboardHooks != null)
        {
            _windowKeyboardHooks?.HookOut();
            _windowKeyboardHooks = null;
        }

        if (_windowsMouseHooks == null && _callbacks.Any(x => x.Value is MouseInput))
        {
            process ??= Process.GetCurrentProcess();
            _windowsMouseHooks = new WindowHooks();
            _windowsMouseHooks.HookIn(process, WH.MOUSE_LL, MouseEventReceived);
        }
        else if (_windowsMouseHooks != null)
        {
            _windowsMouseHooks?.HookOut();
            _windowsMouseHooks = null;
        }

        process?.Dispose();
    }

    private void OnTokenDisposed(object sender, EventArgs e)
    {
        var token = (SubscribeToken)sender;
        token.Disposed -= OnTokenDisposed;
        _callbacks.Remove(token);
        SyncHooks();
    }

    private void KeyboardEventReceived(int code, IntPtr wParam, IntPtr lParam)
    {
        EventReceived(WH.KEYBOARD_LL, wParam, lParam);
    }

    private void MouseEventReceived(int code, IntPtr wParam, IntPtr lParam)
    {
#if NET6_0
        if (wParam.ToInt32() == WM.MOUSEMOVE)
            return;
#else
        if (wParam == WM.MOUSEMOVE)
            return;
#endif
        EventReceived(WH.MOUSE_LL, wParam, lParam);
    }

    private void EventReceived(WH hookType, IntPtr wParam, IntPtr lParam)
    {
        foreach (var callback in _callbacks)
            callback.Value.Handle(hookType, wParam, lParam);
    }
}