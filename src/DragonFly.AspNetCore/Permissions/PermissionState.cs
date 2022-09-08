namespace DragonFly;

/// <summary>
/// PermissionState
/// </summary>
public class PermissionState
{
    private static AsyncLocal<bool> Enabled = new AsyncLocal<bool>();

    public static bool IsEnabled => Enabled.Value;

    public static void Enable() => Enabled.Value = true;

    public static void Disable() => Enabled.Value = false;
}
