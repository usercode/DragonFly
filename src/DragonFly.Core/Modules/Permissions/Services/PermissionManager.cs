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

    private Dictionary<string, string[]> Policies { get; } = new();
    private HashSet<Permission> Items { get; } = new();

    /// <summary>
    /// Add
    /// </summary>
    public PermissionManager Add(Permission permission)
    {
        if (Items.Add(permission))
        {
            Policies[permission.Name] = new[] { permission }.Concat(permission.GetImpliedPermissions())
                                                            .Select(x => x.Name)
                                                            .ToArray();

            Added?.Invoke(permission);
        }

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
    public string[] Get(string permission)
    {
        if (Policies.TryGetValue(permission, out string[]? permissions))
        {
            return permissions;
        }

        throw new Exception($"The permission '{permission}' doesn't exist.");
    }
}
