// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// PermissionState
/// </summary>
public static class PermissionState
{
    private static readonly AsyncLocal<bool> Enabled = new AsyncLocal<bool>();

    public static bool IsEnabled => Enabled.Value;

    public static void Enable() => Enabled.Value = true;

    public static void Disable() => Enabled.Value = false;
}
