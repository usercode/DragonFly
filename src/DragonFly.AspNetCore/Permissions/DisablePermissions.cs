// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// PermissionState
/// </summary>
public class DisablePermissions : IDisposable
{
    private bool _oldValue;

    public DisablePermissions()
    {
        _oldValue = PermissionState.IsEnabled;

        PermissionState.Disable();
    }

    public void Dispose()
    {
        if (_oldValue)
        {
            PermissionState.Enable();
        }
    }
}
