using DragonFly.Core.Permissions;
using DragonFly.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Permissions;

public static class PermissionManagerExtensions
{
    public static void Traverse(this IEnumerable<Permission> permissions, Action<TraverseNode> action)
    {
        TraverseInternal(permissions, new List<Permission>(), action);
    }

    private static void TraverseInternal(IEnumerable<Permission> items, IList<Permission> path, Action<TraverseNode> action)
    {
        foreach (Permission item in items)
        {
            IList<Permission> subPath = path.Concat(new[] { item }).ToList();

            action(new TraverseNode(item, subPath));

            TraverseInternal(item.Childs, subPath, action);
        }
    }
}
