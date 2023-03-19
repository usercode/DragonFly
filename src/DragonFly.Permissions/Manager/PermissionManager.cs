// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonFly.Permissions;

public sealed class TraverseNode
{
    public TraverseNode(PermissionItem permission, IList<PermissionItem> path)
    {
        Permission = permission;
        Path = path;
    }

    public IList<PermissionItem> Path;

    public PermissionItem Permission;
}

/// <summary>
/// PermissionManager
/// </summary>
public class PermissionManager : IPermissionItem
{
    /// <summary>
    /// Default
    /// </summary>
    public static PermissionManager Default { get; } = new PermissionManager();

    public PermissionManager()
    {
        Items = new List<PermissionItem>();
        Policies = new Dictionary<string, IList<string>>();
    }

    public event Action<PermissionItem> Added;

    /// <summary>
    /// Items
    /// </summary>
    public IList<PermissionItem> Items { get; }

    public PermissionItem Add(PermissionItem permissionItem)
    {
        if (_policyBuilt)
        {
            throw new InvalidOperationException("The PermissionManager is already fixed.");
        }

        PermissionItem? found = Items.FirstOrDefault(x => x.Name == permissionItem.Name);

        if (found != null)
        {
            return found;
        }

        Items.Add(permissionItem);

        Added?.Invoke(permissionItem);

        return permissionItem;
    }

    private IDictionary<string, IList<string>> Policies { get; }
    private bool _policyBuilt;

    public IEnumerable<string> GetPolicy(string name)
    {
        if (_policyBuilt == false)
        {
            BuildPolicies();

            _policyBuilt = true;
        }

        if (Policies.TryGetValue(name, out IList<string>? policies))
        {
            return policies;
        }

        return Array.Empty<string>();
    }

    private void BuildPolicies()
    {
        Policies.Clear();

        Items.Traverse(x =>
        {
            Policies.Add(x.Permission.Name, x.Path.Select(p => p.Name).ToList());
        });
    }        
}
