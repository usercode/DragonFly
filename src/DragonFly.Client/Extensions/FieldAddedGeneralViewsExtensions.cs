// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using DragonFly.Client;
using DragonFly.Client.Pages.ContentItems.Fields;
using DragonFly.Client.Pages.ContentItems.Fields.General;

namespace DragonFly;

public static class FieldAddedGeneralViewsExtensions
{
    /// <summary>
    /// Adds TinyMCE view for <typeparamref name="T"/>.
    /// </summary>
    public static IFieldAdded<T> WithTinyMceView<T>(this IFieldAdded<T> field)
        where T : TextBaseField
    {
        ComponentManager.Default.Add(typeof(T), typeof(TinyMceView));

        return field;
    }

    /// <summary>
    /// Adds textarea view for <typeparamref name="T"/>.
    /// </summary>
    public static IFieldAdded<T> WithTextareaView<T>(this IFieldAdded<T> field)
        where T : TextBaseField
    {
        ComponentManager.Default.Add(typeof(T), typeof(TextareaView));

        return field;
    }

    /// <summary>
    /// Adds text input field for <typeparamref name="T"/>.
    /// </summary>
    public static IFieldAdded<T> WithTextInputView<T>(this IFieldAdded<T> field)
        where T : TextBaseField
    {
        ComponentManager.Default.Add(typeof(T), typeof(TextInputView));

        return field;
    }
}
