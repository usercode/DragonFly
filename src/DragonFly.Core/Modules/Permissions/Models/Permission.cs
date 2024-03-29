﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Permission
/// </summary>
public class Permission : IEquatable<Permission>
{
    public const string PolicyPrefix = "DragonFly_";

    public Permission()
    {

    }

    public Permission(PermissionGroup group, string name, string displayName, params Permission[] impliedBy)
    {
        Name = name;
        DisplayName = displayName;
        Group = group;
        ImpliedBy = impliedBy;
    }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// DisplayName
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// Group
    /// </summary>
    public PermissionGroup Group { get; set; }

    /// <summary>
    /// ImpliedBy
    /// </summary>
    public IEnumerable<Permission> ImpliedBy { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is Permission permission)
        {
            return Equals(permission);
        }

        return false;
    }

    public bool Equals(Permission? other)
    {
        if (other is null)
        {
            return false;
        }

        return Name == other.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(Permission), Name);
    }

    public static bool operator ==(Permission permission1, Permission permission2)
    {
        return Equals(permission1, permission2);
    }

    public static bool operator !=(Permission permission1, Permission permission2)
    {
        return Equals(permission1, permission2) == false;
    }

    public override string ToString()
    {
        return Name;
    }
}
