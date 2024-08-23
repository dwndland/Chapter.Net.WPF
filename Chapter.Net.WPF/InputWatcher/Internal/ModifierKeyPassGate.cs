// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ModifierKeyPassGate.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

internal class ModifierKeyPassGate
{
    private readonly ModifierKeys _modifierKeys;

    public ModifierKeyPassGate(ModifierKeys modifierKeys)
    {
        _modifierKeys = modifierKeys;
    }

    public bool Pass()
    {
        if (_modifierKeys == ModifierKeys.None)
            return true;

        return Keyboard.Modifiers == _modifierKeys;
    }
}