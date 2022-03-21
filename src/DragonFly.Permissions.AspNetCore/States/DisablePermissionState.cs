using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore.Services;

/// <summary>
/// DisablePermissionState
/// </summary>
public class DisablePermissionState : IDisposable
{
    public static AsyncLocal<bool> Disabled = new AsyncLocal<bool>();

    public DisablePermissionState()
    {
        Disabled.Value = true;

    }
    public void Dispose()
    {
        Disabled.Value = false;
    }
}
