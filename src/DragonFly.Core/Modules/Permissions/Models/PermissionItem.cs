// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Permission
/// </summary>
public class PermissionItem : IPermissionItem
{
    public PermissionItem()
    {
        Name = string.Empty;
        Description = string.Empty;
        Childs = new List<PermissionItem>();
        SortKey = 0;
    }

    public PermissionItem(string name)
        : this()
    {
        Name = name;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public int SortKey { get; set; }

    public IList<PermissionItem> Childs { get; set; }

    public PermissionItem Add(PermissionItem permissionItem)
    {
        PermissionItem? found = Childs.FirstOrDefault(x => x.Name == permissionItem.Name);

        if (found != null)
        {
            return found;
        }

        Childs.Add(permissionItem);

        return permissionItem;
    }

    public override bool Equals(object? obj)
    {
        if (obj is PermissionItem permissionItem)
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
