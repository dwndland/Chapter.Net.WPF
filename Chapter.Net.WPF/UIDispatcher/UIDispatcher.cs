// -----------------------------------------------------------------------------------------------------------------
// <copyright file="UIDispatcher.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF
{
    /// <summary>
    ///     Class which implements the IDispatcher interface; additionally provides a static access to a dispatcher.
    /// </summary>
    /// <example>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// public void ViewModel1 : ObservableObject
    /// {
    ///     private string _name;
    /// 
    ///     public string Name
    ///     {
    ///         get => _name;
    ///         set => NotifyAndSetIfChanged(ref _name, value);
    ///     }
    /// 
    ///     public void Update(string name)
    ///     {
    ///         UIDispatcher.Current.Invoke(() => Name = name);
    ///     }
    /// }
    /// 
    /// public void ViewModel2 : ObservableObject
    /// {
    ///     private readonly IDispatcher _dispatcher;
    ///     private string _name;
    ///
    ///     public void ViewModel2(IDispatcher dispatcher)
    ///     {
    ///         _dispatcher = dispatcher;
    ///     }
    /// 
    ///     public string Name
    ///     {
    ///         get => _name;
    ///         set => NotifyAndSetIfChanged(ref _name, value);
    ///     }
    /// 
    ///     public void Update(string name)
    ///     {
    ///         _dispatcher.Invoke(() => Name = name);
    ///     }
    /// }
    /// ]]>
    /// </code>
    ///     <code lang="csharp">
    /// <![CDATA[
    /// [TestFixture]
    /// public class ViewModel1Tests
    /// {
    ///     private ViewModel1 _target;
    /// 
    ///     [SetUp]
    ///     public void Setup()
    ///     {
    ///         UIDispatcher.Override(Dispatcher.CurrentDispatcher);
    /// 
    ///         _target = new ViewModel();
    ///     }
    /// 
    ///     [Test]
    ///     public void Update_Called_SetsTheProperty()
    ///     {
    ///         _target.Update("Peter");
    /// 
    ///         Assert.That(_target.Name, Is.EqualTo("Peter"));
    ///     }
    /// }
    /// 
    /// [TestFixture]
    /// public class ViewModel1Tests
    /// {
    ///     private Mock<IDispatcher> _dispatcher;
    ///     private ViewModel2 _target;
    /// 
    ///     [SetUp]
    ///     public void Setup()
    ///     {
    ///         _dispatcher = new Mock<IDispatcher>();
    ///         _target = new ViewModel(_dispatcher.Object);
    ///     }
    /// 
    ///     [Test]
    ///     public void Update_Called_SetsTheProperty()
    ///     {
    ///         _target.Update("Peter");
    ///
    ///         _dispatcher.Verify(x => x.Invoke(It.IsAny<Action>()), Times.Once);
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public sealed class UIDispatcher : IDispatcher
    {
        private static Dispatcher _staticUiDispatcherOverride;
        private static readonly Lazy<Dispatcher> _staticUiDispatcher;
        private readonly Lazy<Dispatcher> _uiDispatcher;

        static UIDispatcher()
        {
            _staticUiDispatcher = new Lazy<Dispatcher>(() =>
            {
                var app = Application.Current;
                return app?.Dispatcher;
            });
        }

        /// <summary>
        ///     Creates a new instance of the <see cref="UIDispatcher" />.
        /// </summary>
        public UIDispatcher()
        {
            _uiDispatcher = new Lazy<Dispatcher>(() =>
            {
                var app = Application.Current;
                return app?.Dispatcher;
            });
        }

        /// <summary>
        ///     Gets the current UI dispatcher. Null if no Application has been created yet and its not yet overwritten by
        ///     <see cref="Override" />.
        /// </summary>
        public static Dispatcher Current => _staticUiDispatcherOverride ?? _staticUiDispatcher.Value;

        /// <inheritdoc />
        public void Invoke(Action callback)
        {
            _uiDispatcher.Value.Invoke(callback);
        }

        /// <inheritdoc />
        public void Invoke(Action callback, DispatcherPriority priority)
        {
            _uiDispatcher.Value.Invoke(callback, priority);
        }

        /// <inheritdoc />
        public void Invoke(Action callback, DispatcherPriority priority, CancellationToken cancellationToken)
        {
            _uiDispatcher.Value.Invoke(callback, priority, cancellationToken);
        }

        /// <inheritdoc />
        public void Invoke(Action callback, DispatcherPriority priority, CancellationToken cancellationToken, TimeSpan timeout)
        {
            _uiDispatcher.Value.Invoke(callback, priority, cancellationToken, timeout);
        }

        /// <inheritdoc />
        public TResult Invoke<TResult>(Func<TResult> callback)
        {
            return _uiDispatcher.Value.Invoke(callback);
        }

        /// <inheritdoc />
        public TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority)
        {
            return _uiDispatcher.Value.Invoke(callback, priority);
        }

        /// <inheritdoc />
        public TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority, CancellationToken cancellationToken)
        {
            return _uiDispatcher.Value.Invoke(callback, priority, cancellationToken);
        }

        /// <inheritdoc />
        public TResult Invoke<TResult>(Func<TResult> callback, DispatcherPriority priority, CancellationToken cancellationToken, TimeSpan timeout)
        {
            return _uiDispatcher.Value.Invoke(callback, priority, cancellationToken, timeout);
        }

        /// <inheritdoc />
        public DispatcherOperation InvokeAsync(Action callback)
        {
            return _uiDispatcher.Value.InvokeAsync(callback);
        }

        /// <inheritdoc />
        public DispatcherOperation InvokeAsync(Action callback, DispatcherPriority priority)
        {
            return _uiDispatcher.Value.InvokeAsync(callback, priority);
        }

        /// <inheritdoc />
        public DispatcherOperation InvokeAsync(Action callback, DispatcherPriority priority, CancellationToken cancellationToken)
        {
            return _uiDispatcher.Value.InvokeAsync(callback, priority, cancellationToken);
        }

        /// <inheritdoc />
        public DispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback)
        {
            return _uiDispatcher.Value.InvokeAsync(callback);
        }

        /// <inheritdoc />
        public DispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, DispatcherPriority priority)
        {
            return _uiDispatcher.Value.InvokeAsync(callback, priority);
        }

        /// <inheritdoc />
        public DispatcherOperation<TResult> InvokeAsync<TResult>(Func<TResult> callback, DispatcherPriority priority, CancellationToken cancellationToken)
        {
            return _uiDispatcher.Value.InvokeAsync(callback, priority, cancellationToken);
        }

        /// <inheritdoc />
        public DispatcherOperation BeginInvoke(Action callback)
        {
            return _uiDispatcher.Value.BeginInvoke(callback);
        }

        /// <inheritdoc />
        public DispatcherOperation BeginInvoke(Action callback, DispatcherPriority priority)
        {
            return _uiDispatcher.Value.BeginInvoke(priority, callback);
        }

        /// <summary>
        ///     Provides the possibility to override the dispatcher returned by <see cref="Current" />.
        /// </summary>
        /// <remarks>This can be set to null to revert back to the original UI dispatcher.</remarks>
        /// <param name="dispatcher">
        ///     The dispatcher to use in <see cref="Current" />. Can be null to revert back to the original UI
        ///     dispatcher.
        /// </param>
        public static void Override(Dispatcher dispatcher)
        {
            _staticUiDispatcherOverride = dispatcher;
        }
    }
}