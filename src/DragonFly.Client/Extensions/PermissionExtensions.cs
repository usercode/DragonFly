// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using DragonFly.Client.Helpers;

namespace DragonFly.Client;

public static class PermissionExtensions
{
    public static IEnumerable<SelectableElement<Permission>> ToSelectableStructure(this IEnumerable<Permission> permissions, Func<Permission, bool> isSelected)
    {
        foreach (Permission permission in permissions)
        {
            yield return new SelectableElement<Permission>(
                                                        isSelected(permission),
                                                        permission);
        }
    }
}
