// -----------------------------------------------------------------------------------------------------------------
// <copyright file="ControlFocus.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     This object gives you the quick and easy possibility to move the current focus to a specific element.
/// </summary>
public static class ControlFocus
{
    /// <summary>
    ///     Gives the focus to the given UIElement.
    /// </summary>
    /// <param name="element">The UIElement which has to get the focus.</param>
    /// <remarks>
    ///     Giving the focus will be done using the target element dispatcher with the
    ///     <see cref="System.Windows.Threading.DispatcherPriority.Render" /> priority.
    /// </remarks>
    public static void GiveFocus(UIElement element)
    {
        element.Dispatcher.BeginInvoke(new Action(delegate
            {
                element.Focus();
                Keyboard.Focus(element);
            }),
            DispatcherPriority.Render);
    }

    /// <summary>
    ///     Gives the focus to the given UIElement with a Callback.
    /// </summary>
    /// <param name="element">The UIElement which has to get the focus.</param>
    /// <param name="actionOnFocus">
    ///     The callback which will be called when the control got the focus. It will called just
    ///     before the element.Focus will called and the KeyboardFocus will be set.
    /// </param>
    /// <remarks>
    ///     Giving the focus will be done using the target element dispatcher with the
    ///     <see cref="System.Windows.Threading.DispatcherPriority.Render" /> priority.
    /// </remarks>
    public static void GiveFocus(UIElement element, Action actionOnFocus)
    {
        element.Dispatcher.BeginInvoke(new Action(() =>
            {
                actionOnFocus();
                element.Focus();
                Keyboard.Focus(element);
            }),
            DispatcherPriority.Render);
    }
}