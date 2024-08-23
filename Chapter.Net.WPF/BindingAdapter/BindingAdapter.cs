// -----------------------------------------------------------------------------------------------------------------
// <copyright file="BindingAdapter.cs" company="dwndland">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

/// <summary>
///     Brings the possibility to modify existing bindings wo be able to bind the parameters in the Binding like Converter,
///     ConverterParameter and so on.
/// </summary>
public class BindingAdapter : FrameworkElement
{
    private readonly FrameworkElement _owner;

    private BindingAdapter(FrameworkElement owner)
    {
        _owner = owner;

        _owner.DataContextChanged += HandleDataContextChanged;
        _owner.Loaded += HandleLoaded;
    }

    private void HandleDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        UpdateExtension();
        UpdateExtensions();
    }

    private void HandleLoaded(object sender, RoutedEventArgs e)
    {
        UpdateExtension();
        UpdateExtensions();
    }

    #region BindingAdapter

    private static BindingAdapter GetBindingAdapter(DependencyObject obj)
    {
        return (BindingAdapter)obj.GetValue(BindingAdapterProperty);
    }

    private static void SetBindingAdapter(DependencyObject obj, BindingAdapter value)
    {
        obj.SetValue(BindingAdapterProperty, value);
    }

    private static readonly DependencyProperty BindingAdapterProperty =
        DependencyProperty.RegisterAttached("BindingAdapter", typeof(BindingAdapter), typeof(BindingAdapter), new PropertyMetadata(null));

    #endregion BindingAdapter

    #region BindingExtensions

    /// <summary>
    ///     Gets the extension collection which modifies a binding.
    /// </summary>
    /// <param name="obj">The element from which the property value is read.</param>
    /// <returns>The BindingExtensions property value for the element.</returns>
    public static BindingExtensionCollection GetBindingExtensions(DependencyObject obj)
    {
        var extension = (BindingExtensionCollection)obj.GetValue(BindingExtensionsProperty);
        if (extension == null)
        {
            var extensionsCollection = new BindingExtensionCollection();
            obj.SetValue(BindingExtensionsProperty, extensionsCollection);
        }

        return extension;
    }

    /// <summary>
    ///     Sets the extensions collection which modifies a binding.
    /// </summary>
    /// <param name="obj">The element to which the attached property is written.</param>
    /// <param name="value">The needed DW.WPFToolkit.Helpers.BindingAdapter.BindingExtensions value.</param>
    public static void SetBindingExtensions(DependencyObject obj, BindingExtensionCollection value)
    {
        obj.SetValue(BindingExtensionsProperty, value);
    }

    /// <summary>
    ///     Identifies the <see cref="GetBindingExtensions(DependencyObject)" />
    ///     <see cref="SetBindingExtensions(DependencyObject, BindingExtensionCollection)" /> attached property.
    /// </summary>
    public static readonly DependencyProperty BindingExtensionsProperty =
        DependencyProperty.RegisterAttached("BindingExtensions", typeof(BindingExtensionCollection), typeof(BindingAdapter), new PropertyMetadata(OnBindingExtensionsChanged));

    private static void OnBindingExtensionsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var container = GetBindingAdapter(d);
        if (container != null)
            return;

        container = new BindingAdapter((FrameworkElement)d);
        SetBindingAdapter(d, container);
    }

    private void UpdateExtensions()
    {
        var extensions = GetBindingExtensions(_owner);
        if (extensions == null)
            return;

        foreach (var extension in extensions)
        {
            extension.DataContext = _owner.DataContext;
            extension.Owner = _owner;
        }
    }

    #endregion BindingExtensions

    #region BindingExtension

    /// <summary>
    ///     Gets the binding extension which modifies a binding.
    /// </summary>
    /// <param name="obj">The element from which the property value is read.</param>
    /// <returns>The BindingExtension property value for the element.</returns>
    public static BindingExtension GetBindingExtension(DependencyObject obj)
    {
        return (BindingExtension)obj.GetValue(BindingExtensionProperty);
    }

    /// <summary>
    ///     Sets the binding extension which modifies a binding.
    /// </summary>
    /// <param name="obj">The element to which the attached property is written.</param>
    /// <param name="value">The needed BindingExtension value.</param>
    public static void SetBindingExtension(DependencyObject obj, BindingExtension value)
    {
        obj.SetValue(BindingExtensionProperty, value);
    }

    /// <summary>
    ///     Identifies the <see cref="GetBindingExtension(DependencyObject)" />
    ///     <see cref="SetBindingExtension(DependencyObject, BindingExtension)" /> attached property.
    /// </summary>
    public static readonly DependencyProperty BindingExtensionProperty =
        DependencyProperty.RegisterAttached("BindingExtension", typeof(BindingExtension), typeof(BindingAdapter), new PropertyMetadata(OnBindingExtensionChanged));

    private static void OnBindingExtensionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var container = GetBindingAdapter(d);
        if (container != null)
            return;

        container = new BindingAdapter((FrameworkElement)d);
        SetBindingAdapter(d, container);
    }

    private void UpdateExtension()
    {
        var extension = GetBindingExtension(_owner);
        if (extension == null)
            return;

        extension.DataContext = _owner.DataContext;
        extension.Owner = _owner;
    }

    #endregion BindingExtension
}