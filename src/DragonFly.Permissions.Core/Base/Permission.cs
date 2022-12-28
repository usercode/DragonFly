// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;
using System.Linq;

namespace DragonFly;

/// <summary>
/// Permission
/// </summary>
public class Permission : IPermission
{
    public Permission()
    {
        Name = string.Empty;
        Description = string.Empty;
        Childs = new List<Permission>();
        SortKey = 0;
    }

    public Permission(string name)
        : this()
    {
        Name = name;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public int SortKey { get; set; }

    public IList<Permission> Childs { get; set; }

    public Permission Add(Permission permissionItem)
    {
        Permission? found = Childs.FirstOrDefault(x => x.Name == permissionItem.Name);

        if (found != null)
        {
            return found;
        }

        Childs.Add(permissionItem);

        return permissionItem;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Permission permissionItem)
        {
            return Name == permissionItem.Name;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
