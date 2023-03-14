// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// SuppressPermissions
/// </summary>
public struct SuppressPermissions : IDisposable
{
    private readonly bool _restore;

    internal SuppressPermissions(bool restore)
    {
        _restore = restore;
    }

    public void Dispose()
    {
        if (_restore)
        {
            PermissionState.Enable();
        }
    }
}
