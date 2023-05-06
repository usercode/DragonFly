// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public static class PermissionExtensions
{
    public static IEnumerable<string> GetAllImpliedPermissions(this Permission permission)
    {
        yield return permission.Name;

        foreach (Permission p in permission.ImpliedBy)
        {
            foreach (string a in GetAllImpliedPermissions(p))
            {
                yield return a;
            }
        }
    }
}
