// -----------------------------------------------------------------------------------------------------------------
// <copyright file="MouseInput.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows.Input;
using Chapter.Net.WinAPI.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF
{
    /// <summary>
    ///     Creates a filter and callback for native mouse events.
    /// </summary>
    public class MouseInput : Input
    {
        private readonly Action<MouseEventArgs> _callback;
        private readonly ModifierKeyPassGate _modifierKeyPassGate;
        private readonly MouseKeyPassGate _mouseKeyPassGate;

        /// <summary>
        ///     Creates a new MouseInput.
        /// </summary>
        /// <param name="mouseAction">The mouse action.</param>
        /// <param name="callback">The callback.</param>
        public MouseInput(MouseAction mouseAction, Action<MouseEventArgs> callback)
        {
            _mouseKeyPassGate = new MouseKeyPassGate(mouseAction);
            _modifierKeyPassGate = new ModifierKeyPassGate(ModifierKeys.None);
            _callback = callback;
        }

        /// <summary>
        ///     Creates a new MouseInput.
        /// </summary>
        /// <param name="mouseAction">The mouse action.</param>
        /// <param name="modifierKeys">The modifier keys pressed while the mouse action happened.</param>
        /// <param name="callback">The callback.</param>
        public MouseInput(MouseAction mouseAction, ModifierKeys modifierKeys, Action<MouseEventArgs> callback)
        {
            _mouseKeyPassGate = new MouseKeyPassGate(mouseAction);
            _modifierKeyPassGate = new ModifierKeyPassGate(modifierKeys);
            _callback = callback;
        }

        internal override void Handle(WH hookType, IntPtr wParam, IntPtr lParam)
        {
            if (wParam.ToInt32() == WM.MOUSEMOVE)
                return;

            if (hookType == WH.MOUSE_LL &&
                _mouseKeyPassGate.Pass(wParam, lParam) &&
                _modifierKeyPassGate.Pass())
            {
                var args = new MouseEventArgs(Keyboard.Modifiers);
                _callback(args);
            }
        }
    }
}