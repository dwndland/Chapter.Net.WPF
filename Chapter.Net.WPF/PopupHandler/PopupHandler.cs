// -----------------------------------------------------------------------------------------------------------------
// <copyright file="PopupHandler.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using Chapter.Net.WinAPI;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     This objects help to determine when a popup has to be closed. This can be by clicking somewhere else, clicking in
///     the title bar or moving the window.
/// </summary>
public class PopupHandler
{
    private Action _closeMethod;
    private UIElement _observedControl;
    private WindowObserver _observer;

    /// <summary>
    ///     Starts an observing of the window which contains the control to determine when the item has to be closed.
    /// </summary>
    /// <param name="observedControl">The control which owner window has to be observed.</param>
    /// <param name="closeMethod">
    ///     The close callback. This gets invoked when the owner window has send notifications to close
    ///     the popup.
    /// </param>
    /// <exception cref="ArgumentNullException">observedControl is null.</exception>
    /// <exception cref="ArgumentNullException">closeMethod is null.</exception>
    public void AutoClose(UIElement observedControl, Action closeMethod)
    {
        _observedControl = observedControl ?? throw new ArgumentNullException(nameof(observedControl));
        _closeMethod = closeMethod ?? throw new ArgumentNullException(nameof(closeMethod));

        var ownerWindow = Window.GetWindow(observedControl);
        if (ownerWindow != null)
        {
            _observer = new WindowObserver(ownerWindow);

            _observer.AddCallbackFor(WM.NCLBUTTONDOWN, _ => CallMethod());
            _observer.AddCallbackFor(WM.LBUTTONDOWN, _ => CallMethod());
            _observer.AddCallbackFor(WM.KILLFOCUS, _ => CallMethod());
        }
    }

    private void CallMethod()
    {
        if (!_observedControl.IsMouseOver)
            _closeMethod();
    }
}