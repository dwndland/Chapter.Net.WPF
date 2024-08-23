// -----------------------------------------------------------------------------------------------------------------
// <copyright file="KeyPressState.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Represents a press state.
/// </summary>
public enum KeyPressState
{
    /// <summary>
    ///     The key was pressed.
    /// </summary>
    Down,

    /// <summary>
    ///     The key was released.
    /// </summary>
    Up
}