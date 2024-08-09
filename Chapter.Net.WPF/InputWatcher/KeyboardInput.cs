// -----------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardInput.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Input;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Creates a filter and callback for native keyboard events.
/// </summary>
public class KeyboardInput : Input
{
    private readonly Action<KeyboardEventArgs> _callback;
    private readonly KeyPassGate _keyPassGate;
    private readonly ModifierKeyPassGate _modifierKeyPassGate;

    /// <summary>
    ///     Creates a new KeyboardInput.
    /// </summary>
    /// <param name="key">The key to observe.</param>
    /// <param name="callback">The callback if it happened.</param>
    public KeyboardInput(Key key, Action<KeyboardEventArgs> callback)
    {
        _keyPassGate = new KeyPassGate(key, KeyPressState.Down);
        _modifierKeyPassGate = new ModifierKeyPassGate(ModifierKeys.None);
        _callback = callback;
    }

    /// <summary>
    ///     Creates a new KeyboardInput.
    /// </summary>
    /// <param name="key">The key to observe.</param>
    /// <param name="modifierKeys">The modifier keys which needs to be pressed.</param>
    /// <param name="callback">The callback if it happened.</param>
    public KeyboardInput(Key key, ModifierKeys modifierKeys, Action<KeyboardEventArgs> callback)
    {
        _keyPassGate = new KeyPassGate(key, KeyPressState.Down);
        _modifierKeyPassGate = new ModifierKeyPassGate(modifierKeys);
        _callback = callback;
    }

    /// <summary>
    ///     Creates a new KeyboardInput.
    /// </summary>
    /// <param name="key">The key to observe.</param>
    /// <param name="keyPressState">The state the key needs to have.</param>
    /// <param name="callback">The callback if it happened.</param>
    public KeyboardInput(Key key, KeyPressState keyPressState, Action<KeyboardEventArgs> callback)
    {
        _keyPassGate = new KeyPassGate(key, keyPressState);
        _modifierKeyPassGate = new ModifierKeyPassGate(ModifierKeys.None);
        _callback = callback;
    }

    /// <summary>
    ///     Creates a new KeyboardInput.
    /// </summary>
    /// <param name="key">The key to observe.</param>
    /// <param name="keyPressState">The state the key needs to have.</param>
    /// <param name="modifierKeys">The modifier keys which needs to be pressed.</param>
    /// <param name="callback">The callback if it happened.</param>
    public KeyboardInput(Key key, KeyPressState keyPressState, ModifierKeys modifierKeys, Action<KeyboardEventArgs> callback)
    {
        _keyPassGate = new KeyPassGate(key, keyPressState);
        _modifierKeyPassGate = new ModifierKeyPassGate(modifierKeys);
        _callback = callback;
    }

    internal override void Handle(WH hookType, IntPtr wParam, IntPtr lParam)
    {
        if (hookType == WH.KEYBOARD_LL &&
            _keyPassGate.Pass(wParam, lParam) &&
            _modifierKeyPassGate.Pass())
        {
            var args = new KeyboardEventArgs(_keyPassGate.Key, _keyPassGate.KeyPressState, Keyboard.Modifiers);
            _callback(args);
        }
    }
}