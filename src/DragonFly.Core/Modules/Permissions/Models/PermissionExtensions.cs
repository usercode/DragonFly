﻿// Copyright (c) usercode
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
        static IEnumerable<Permission> BuildImpliedPermissionsInternal(Permission permission, HashSet<Permission> hashSet)
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

    /// <summary>
    /// Adds the policy prefix to the name.
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    public static string GetPolicyName(this Permission permission)
    {
        return $"{Permission.PolicyPrefix}{permission.Name}";
    }
}
