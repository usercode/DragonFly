// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;

namespace DragonFly.Client;

public static class SelectableElementExtensions
{
    public static IEnumerable<SelectableElement<T>> ToSelectableElement<T>(this IEnumerable<T> permissions, Func<T, bool> isSelected)
    {
        foreach (T permission in permissions)
        {
            yield return new SelectableElement<T>(isSelected(permission), permission);
        }
    }
}
