// -----------------------------------------------------------------------------------------------------------------
// <copyright file="IDispatcher.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Windows.Threading;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Provides access to the UI dispatcher in the WPF application environment.
/// </summary>
public interface IDispatcher
{
    /// <summary>
    ///     Executes the specified Action synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <remarks>Note that the default priority is DispatcherPriority.Send.</remarks>
    void Invoke(Action callback);

    /// <summary>
    ///     Executes the specified Action synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    void Invoke(Action callback, DispatcherPriority priority);

    /// <summary>
    ///     Executes the specified Action synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used to cancel the operation. If the operation has not
    ///     started, it will be aborted when the cancellation token is canceled. If the operation has started, the operation
    ///     can cooperate with the cancellation request.
    /// </param>
    void Invoke(Action callback, DispatcherPriority priority, CancellationToken cancellationToken);

    /// <summary>
    ///     Executes the specified Action synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used to cancel the operation. If the operation has not
    ///     started, it will be aborted when the cancellation token is canceled.  If the operation has started, the operation
    ///     can cooperate with the cancellation request.
    /// </param>
    /// <param name="timeout">
    ///     The minimum amount of time to wait for the operation to start. Once the operation has started, it
    ///     will complete before this method returns.
    /// </param>
    void Invoke(Action callback, DispatcherPriority priority, CancellationToken cancellationToken, TimeSpan timeout);

    /// <summary>
    ///     Executes the specified Func&lt;TResult&gt; synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">A Func&lt;TResult&gt; delegate to invoke through the dispatcher.</param>
    /// <returns>The return value from the delegate being invoked.</returns>
    /// <remarks>Note that the default priority is DispatcherPriority.Send.</remarks>
    TResult Invoke<TResult>(Func<TResult> callback);

    /// <summary>
    ///     Executes the specified Func&lt;TResult&gt; synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">A Func&lt;TResult&gt; delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <returns>The return value from the delegate being invoked.</returns>
    TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority);

    /// <summary>
    ///     Executes the specified Func&lt;TResult&gt; synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">A Func&lt;TResult&gt; delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used to cancel the operation. If the operation has not
    ///     started, it will be aborted when the cancellation token is canceled.  If the operation has started, the operation
    ///     can cooperate with the cancellation request.
    /// </param>
    /// <returns>The return value from the delegate being invoked.</returns>
    TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority, CancellationToken cancellationToken);

    /// <summary>
    ///     Executes the specified Func&lt;TResult&gt; synchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">A Func&lt;TResult&gt; delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used to cancel the operation. If the operation has not
    ///     started, it will be aborted when the cancellation token is canceled.  If the operation has started, the operation
    ///     can cooperate with the cancellation request.
    /// </param>
    /// <param name="timeout">
    ///     The minimum amount of time to wait for the operation to start. Once the operation has started, it
    ///     will complete before this method returns.
    /// </param>
    /// <returns>The return value from the delegate being invoked.</returns>
    TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority, CancellationToken cancellationToken, TimeSpan timeout);

    /// <summary>
    ///     Executes the specified Action asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <returns>An operation representing the queued delegate to be invoked.</returns>
    /// <remarks>Note that the default priority is DispatcherPriority.Normal.</remarks>
    DispatcherOperation InvokeAsync(Action callback);

    /// <summary>
    ///     Executes the specified Action asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <returns>An operation representing the queued delegate to be invoked.</returns>
    /// <returns>An operation representing the queued delegate to be invoked.</returns>
    DispatcherOperation InvokeAsync(Action callback, DispatcherPriority priority);

    /// <summary>
    ///     Executes the specified Action asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used to cancel the operation. If the operation has not
    ///     started, it will be aborted when the cancellation token is canceled.  If the operation has started, the operation
    ///     can cooperate with the cancellation request.
    /// </param>
    /// <returns>An operation representing the queued delegate to be invoked.</returns>
    DispatcherOperation InvokeAsync(Action callback, DispatcherPriority priority, CancellationToken cancellationToken);

    /// <summary>
    ///     Executes the specified Func&lt;TResult&gt; asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">A Func&lt;TResult&gt; delegate to invoke through the dispatcher.</param>
    /// <returns>An operation representing the queued delegate to be invoked.</returns>
    /// <remarks>Note that the default priority is DispatcherPriority.Normal.</remarks>
    DispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback);

    /// <summary>
    ///     Executes the specified Func&lt;TResult&gt; asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">A Func&lt;TResult&gt; delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <returns>An operation representing the queued delegate to be invoked.</returns>
    DispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, DispatcherPriority priority);

    /// <summary>
    ///     Executes the specified Func&lt;TResult&gt; asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">A Func&lt;TResult&gt; delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending operations in the Dispatcher.
    /// </param>
    /// <param name="cancellationToken">
    ///     A cancellation token that can be used to cancel the operation. If the operation has not
    ///     started, it will be aborted when the cancellation token is canceled.  If the operation has started, the operation
    ///     can cooperate with the cancellation request.
    /// </param>
    /// <returns>An operation representing the queued delegate to be invoked.</returns>
    DispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, DispatcherPriority priority, CancellationToken cancellationToken);

    /// <summary>
    ///     Executes the specified action asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <returns>An IAsyncResult object that represents the result of the BeginInvoke operation.</returns>
    DispatcherOperation BeginInvoke(Action callback);

    /// <summary>
    ///     Executes the specified action asynchronously on the thread that the Dispatcher was created on.
    /// </summary>
    /// <param name="callback">An Action delegate to invoke through the dispatcher.</param>
    /// <param name="priority">
    ///     The priority that determines in what order the specified callback is invoked relative to the
    ///     other pending methods in the Dispatcher.
    /// </param>
    /// <returns>An IAsyncResult object that represents the result of the BeginInvoke operation.</returns>
    DispatcherOperation BeginInvoke(Action callback, DispatcherPriority priority);
}