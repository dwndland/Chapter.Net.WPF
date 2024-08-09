// -----------------------------------------------------------------------------------------------------------------
// <copyright file="KeyPassGate.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

internal class KeyPassGate
{
    public KeyPassGate(Key key, KeyPressState keyPressState)
    {
        Key = key;
        KeyPressState = keyPressState;
    }

    public Key Key { get; }
    public KeyPressState KeyPressState { get; }

    public bool Pass(IntPtr wParam, IntPtr lParam)
    {
        var key = KeyInterop.KeyFromVirtualKey(Marshal.ReadInt32(lParam));
        if (key != Key)
            return false;

        switch (KeyPressState)
        {
            case KeyPressState.Down:
                return wParam.ToInt32() == WM.KEYDOWN || wParam.ToInt32() == WM.SYSKEYDOWN;
            case KeyPressState.Up:
                return wParam.ToInt32() == WM.KEYUP || wParam.ToInt32() == WM.SYSKEYUP;
            default:
                return false;
        }
    }
}