// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Permissions;

public sealed class TraverseNode
{
    public TraverseNode(Permission permission, IList<Permission> path)
    {
        Permission = permission;
        Path = path;
    }

    public IList<Permission> Path;

    public Permission Permission;
}

/// <summary>
/// PermissionManager
/// </summary>
public sealed class PermissionManager
{
    /// <summary>
    /// Default
    /// </summary>
    public static PermissionManager Default { get; } = new PermissionManager();

    public event Action<Permission>? Added;

    public PermissionManager Add(Permission permission)
    {
        Items.Add(permission);

        Policies.Add(permission.Name, GetImpliedPermissions(permission).Select(x=> x.Name).ToArray());

        Added?.Invoke(permission);

        return this;
    }

    private IDictionary<string, IEnumerable<string>> Policies { get; } = new Dictionary<string, IEnumerable<string>>();  
    private IList<Permission> Items { get; } = new List<Permission>();

    /// <summary>
    /// GetAll
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Permission> GetAll() => Items;

    /// <summary>
    /// GetPolicy
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public IEnumerable<string> GetPolicy(string name)
    {
        if (Policies.TryGetValue(name, out IEnumerable<string>? policies))
        {
            return policies;
        }

        return Array.Empty<string>();
    }

    private IEnumerable<Permission> GetImpliedPermissions(Permission permission)
    {
        return GetImpliedPermissionsInternal(permission).Distinct().ToArray();
    }

    private IEnumerable<Permission> GetImpliedPermissionsInternal(Permission permission)
    {
        yield return permission;

        foreach (Permission p in permission.ImpliedBy)
        {
            foreach (Permission a in GetImpliedPermissions(p))
            {
                yield return a;
            }
        }
    }
}
