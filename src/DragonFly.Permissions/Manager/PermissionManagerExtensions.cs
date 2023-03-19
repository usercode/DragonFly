// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonFly.Permissions;

public static class PermissionManagerExtensions
{
    public static void Traverse(this IEnumerable<PermissionItem> permissions, Action<TraverseNode> action)
    {
        TraverseInternal(permissions, new List<PermissionItem>(), action);
    }

    private static void TraverseInternal(IEnumerable<PermissionItem> items, IList<PermissionItem> path, Action<TraverseNode> action)
    {
        foreach (PermissionItem item in items)
        {
            IList<PermissionItem> subPath = path.Concat(new[] { item }).ToList();

            action(new TraverseNode(item, subPath));

            TraverseInternal(item.Childs, subPath, action);
        }
    }
}
