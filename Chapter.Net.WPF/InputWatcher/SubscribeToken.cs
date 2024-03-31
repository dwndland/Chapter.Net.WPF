// -----------------------------------------------------------------------------------------------------------------
// <copyright file="SubscribeToken.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF
{
    /// <summary>
    ///     The token received after <see cref="IInputWatcher.Observe" /> got called to free the callback.
    /// </summary>
    public class SubscribeToken : IEquatable<SubscribeToken>, IDisposable
    {
        private readonly Guid _guid;

        internal SubscribeToken()
        {
            _guid = Guid.NewGuid();
        }

        /// <summary>
        ///     Frees the callback on the <see cref="IInputWatcher" />.
        /// </summary>
        public void Dispose()
        {
            Disposed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Compares this SubscribeToken with another.
        /// </summary>
        /// <param name="other">The other SubscribeToken.</param>
        /// <returns>True if its the same token; otherwise false.</returns>
        public bool Equals(SubscribeToken other)
        {
            return _guid.Equals(other._guid);
        }

        internal event EventHandler Disposed;
    }
}