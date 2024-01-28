// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using DragonFly.Client;
using DragonFly.Client.Pages.ContentItems.Fields;

namespace DragonFly;

public static class FieldAdded2Extensions
{
    /// <summary>
    /// Adds TinyMCE view for <typeparamref name="T"/>.
    /// </summary>
    public static IFieldAdded<T> WithTinyMceView<T>(this IFieldAdded<T> field)
        where T : TextBaseField
    {
        ComponentManager.Default.Add(typeof(T), typeof(TinyMceFieldView));

        return field;
    }
}
