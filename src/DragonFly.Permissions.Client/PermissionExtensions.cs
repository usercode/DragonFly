// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using DragonFly.Client.Helpers;

namespace DragonFly.Permissions.Client;

public static class PermissionExtensions
{
    public static IEnumerable<SelectableElementTree<Permission>> ToSelectableStructure(this IEnumerable<Permission> permissions, Func<Permission, bool> isSelected)
    {
        foreach (Permission permission in permissions
                                                    .OrderBy(x => x.SortKey)
                                                    .ThenBy(x => x.Name))
        {
            yield return new SelectableElementTree<Permission>(
                                    isSelected(permission),
                                    permission,
                                    ToSelectableStructure(permission.Childs, isSelected).ToList())
                    .EnableActivePath();
        }
    }
}
