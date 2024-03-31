// -----------------------------------------------------------------------------------------------------------------
// <copyright file="KeyboardEventArgs.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Input;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF
{
    /// <summary>
    ///     The event parameter after key events got received.
    /// </summary>
    public class KeyboardEventArgs
    {
        internal KeyboardEventArgs(Key key, KeyPressState keyPressState, ModifierKeys modifierKeys)
        {
            Key = key;
            KeyPressState = keyPressState;
            ModifierKeys = modifierKeys;
        }

        /// <summary>
        ///     Gets observed key.
        /// </summary>
        public Key Key { get; }

        /// <summary>
        ///     Gets the key press state.
        /// </summary>
        public KeyPressState KeyPressState { get; }

        /// <summary>
        ///     Gets the pressed modifiers.
        /// </summary>
        public ModifierKeys ModifierKeys { get; }
    }
}