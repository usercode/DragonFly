// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore.Permissions;

public class DisableAuthorizationState : IDisposable
{
    internal static readonly AsyncLocal<bool> IsEnabled = new AsyncLocal<bool>();

    public DisableAuthorizationState()
    {
        OldValue = IsEnabled.Value;

        IsEnabled.Value = true;
    }

    /// <summary>
    /// OldPrincipal
    /// </summary>
    private bool OldValue { get; }

    public void Dispose()
    {
        IsEnabled.Value = OldValue;
    }
}
