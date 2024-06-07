// -----------------------------------------------------------------------------------------------------------------
// <copyright file="BindingHelper.cs" company="my-libraries">
//     Copyright (c) David Wendland. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------------------------------------------------

using System.Windows.Data;

// ReSharper disable once CheckNamespace

namespace Chapter.Net.WPF;

internal static class BindingHelper
{
    internal static Binding Clone(this Binding binding)
    {
        return new Binding
        {
            UpdateSourceTrigger = binding.UpdateSourceTrigger,
            ValidatesOnDataErrors = binding.ValidatesOnDataErrors,
            Mode = binding.Mode,
            Path = binding.Path,
            AsyncState = binding.AsyncState,
            BindingGroupName = binding.BindingGroupName,
            BindsDirectlyToSource = binding.BindsDirectlyToSource,
            Converter = binding.Converter,
            ConverterCulture = binding.ConverterCulture,
            ConverterParameter = binding.ConverterParameter,
            ElementName = binding.ElementName,
            FallbackValue = binding.FallbackValue,
            IsAsync = binding.IsAsync,
            NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated,
            NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated,
            NotifyOnValidationError = binding.NotifyOnValidationError,
            StringFormat = binding.StringFormat,
            TargetNullValue = binding.TargetNullValue,
            UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter,
            ValidatesOnExceptions = binding.ValidatesOnExceptions,
            XPath = binding.XPath
            //ValidationRules = binding.ValidationRules
        };
    }

    internal static void CopyInto(this Binding binding, BindingExtension target)
    {
        target.UpdateSourceTrigger = binding.UpdateSourceTrigger;
        target.ValidatesOnDataErrors = binding.ValidatesOnDataErrors;
        target.Mode = binding.Mode;
        target.Path = binding.Path;
        target.AsyncState = binding.AsyncState;
        target.BindingGroupName = binding.BindingGroupName;
        target.BindsDirectlyToSource = binding.BindsDirectlyToSource;
        target.Converter = binding.Converter;
        target.ConverterCulture = binding.ConverterCulture;
        target.ConverterParameter = binding.ConverterParameter;
        target.ElementName = binding.ElementName;
        target.FallbackValue = binding.FallbackValue;
        target.IsAsync = binding.IsAsync;
        target.NotifyOnSourceUpdated = binding.NotifyOnSourceUpdated;
        target.NotifyOnTargetUpdated = binding.NotifyOnTargetUpdated;
        target.NotifyOnValidationError = binding.NotifyOnValidationError;
        target.StringFormat = binding.StringFormat;
        target.TargetNullValue = binding.TargetNullValue;
        target.UpdateSourceExceptionFilter = binding.UpdateSourceExceptionFilter;
        target.ValidatesOnExceptions = binding.ValidatesOnExceptions;
        target.XPath = binding.XPath;
    }

    internal static void Take(this Binding binding, BindingExtension source)
    {
        var bindingDefaults = new Binding();
        if (source.UpdateSourceTrigger != bindingDefaults.UpdateSourceTrigger)
            binding.UpdateSourceTrigger = source.UpdateSourceTrigger;
        if (source.ValidatesOnDataErrors != bindingDefaults.ValidatesOnDataErrors)
            binding.ValidatesOnDataErrors = source.ValidatesOnDataErrors;
        if (source.Mode != bindingDefaults.Mode)
            binding.Mode = source.Mode;
        if (source.Path != bindingDefaults.Path)
            binding.Path = source.Path;
        if (source.AsyncState != bindingDefaults.AsyncState)
            binding.AsyncState = source.AsyncState;
        if (source.BindingGroupName != bindingDefaults.BindingGroupName)
            binding.BindingGroupName = source.BindingGroupName;
        if (source.BindsDirectlyToSource != bindingDefaults.BindsDirectlyToSource)
            binding.BindsDirectlyToSource = source.BindsDirectlyToSource;
        if (source.Converter != bindingDefaults.Converter)
            binding.Converter = source.Converter;
        if (!Equals(source.ConverterCulture, bindingDefaults.ConverterCulture))
            binding.ConverterCulture = source.ConverterCulture;
        if (source.ConverterParameter != bindingDefaults.ConverterParameter)
            binding.ConverterParameter = source.ConverterParameter;
        if (source.ElementName != bindingDefaults.ElementName)
            binding.ElementName = source.ElementName;
        if (source.FallbackValue != bindingDefaults.FallbackValue)
            binding.FallbackValue = source.FallbackValue;
        if (source.IsAsync != bindingDefaults.IsAsync)
            binding.IsAsync = source.IsAsync;
        if (source.NotifyOnSourceUpdated != bindingDefaults.NotifyOnSourceUpdated)
            binding.NotifyOnSourceUpdated = source.NotifyOnSourceUpdated;
        if (source.NotifyOnTargetUpdated != bindingDefaults.NotifyOnTargetUpdated)
            binding.NotifyOnTargetUpdated = source.NotifyOnTargetUpdated;
        if (source.NotifyOnValidationError != bindingDefaults.NotifyOnValidationError)
            binding.NotifyOnValidationError = source.NotifyOnValidationError;
        if (source.StringFormat != bindingDefaults.StringFormat)
            binding.StringFormat = source.StringFormat;
        if (source.TargetNullValue != bindingDefaults.TargetNullValue)
            binding.TargetNullValue = source.TargetNullValue;
        if (source.UpdateSourceExceptionFilter != bindingDefaults.UpdateSourceExceptionFilter)
            binding.UpdateSourceExceptionFilter = source.UpdateSourceExceptionFilter;
        if (source.ValidatesOnExceptions != bindingDefaults.ValidatesOnExceptions)
            binding.ValidatesOnExceptions = source.ValidatesOnExceptions;
        if (source.XPath != bindingDefaults.XPath)
            binding.XPath = source.XPath;
    }
}