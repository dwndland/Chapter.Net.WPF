// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IInputWatcher.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Listens to native windows events and filters them by the given condition.
/// </summary>
public interface IInputWatcher : IDisposable
{
    /// <summary>
    ///     Gets the current observing state.
    /// </summary>
    bool IsObserving { get; }

    /// <summary>
    ///     Registers a callback with filters for the windows messages.
    /// </summary>
    /// <param name="input">The user input to observe.</param>
    /// <returns>The disposable subscribe token to free the callback.</returns>
    SubscribeToken Observe(Input input);

    /// <summary>
    ///     Starts listen to the windows events.
    /// </summary>
    void Start();

    /// <summary>
    ///     Stops listen to the windows events.
    /// </summary>
    void Stop();
}