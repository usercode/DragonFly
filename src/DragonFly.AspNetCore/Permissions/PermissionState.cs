namespace DragonFly;

/// <summary>
/// PermissionState
/// </summary>
public class PermissionState : IDisposable
{
    private static AsyncLocal<bool> Enabled = new AsyncLocal<bool>();

    public static bool IsEnabled => Enabled.Value;

    public static void Enable() => Enabled.Value = true;

    private bool _oldValue;

    public PermissionState()
    {
        _oldValue = Enabled.Value;
    }

    public void Dispose()
    {
        Enabled.Value = _oldValue;
    }
}
