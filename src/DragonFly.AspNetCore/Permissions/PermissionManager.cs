// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

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

    private IDictionary<string, string[]> Policies { get; } = new Dictionary<string, string[]>();
    private HashSet<Permission> Items { get; } = new HashSet<Permission>();

    /// <summary>
    /// Add
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    internal PermissionManager Add(Permission permission)
    {
        Items.Add(permission);
        Policies.Add(permission.Name, BuildImpliedPermissions(permission).Select(x => x.Name).ToArray());

        Added?.Invoke(permission);

        return this;
    }

    /// <summary>
    /// Gets all permissions.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Permission> GetAll() => Items;

    /// <summary>
    /// Gets self and implied permissions.
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string[] Get(string name)
    {
        if (Policies.TryGetValue(name, out string[]? permissions))
        {
            return permissions;
        }

        return Array.Empty<string>();
    }

    /// <summary>
    /// GetImpliedPermissions
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    private IEnumerable<Permission> BuildImpliedPermissions(Permission permission)
    {
        IEnumerable<Permission> BuildImpliedPermissionsInternal(Permission permission)
        {
            yield return permission;

            foreach (Permission p in permission.ImpliedBy)
            {
                foreach (Permission a in BuildImpliedPermissionsInternal(p))
                {
                    yield return a;
                }
            }
        }

        return BuildImpliedPermissionsInternal(permission).Distinct().ToArray();
    }
}
