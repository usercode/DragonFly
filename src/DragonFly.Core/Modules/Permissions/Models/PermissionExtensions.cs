// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class PermissionExtensions
{
    /// <summary>
    /// GetImpliedPermissions
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static IEnumerable<Permission> GetImpliedPermissions(this Permission permission)
    {
        IEnumerable<Permission> BuildImpliedPermissionsInternal(Permission permission, HashSet<Permission> hashSet)
        {
            foreach (Permission p in permission.ImpliedBy)
            {
                if (hashSet.Add(p))
                {
                    yield return p;

                    foreach (Permission a in BuildImpliedPermissionsInternal(p, hashSet))
                    {
                        yield return a;
                    }
                }
            }
        }

        HashSet<Permission> hashSet = new HashSet<Permission>();

        return BuildImpliedPermissionsInternal(permission, hashSet);
    }
}
