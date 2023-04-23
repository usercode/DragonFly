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
    public static IEnumerable<SelectableElementTree<PermissionItem>> ToSelectableStructure(this IEnumerable<PermissionItem> permissions, Func<PermissionItem, bool> isSelected)
    {
        foreach (PermissionItem permission in permissions
                                                    .OrderBy(x => x.SortKey)
                                                    .ThenBy(x => x.Name))
        {
            yield return new SelectableElementTree<PermissionItem>(
                                    isSelected(permission),
                                    permission,
                                    ToSelectableStructure(permission.Childs, isSelected).ToList())
                    .EnableActivePath();
        }
    }
}
