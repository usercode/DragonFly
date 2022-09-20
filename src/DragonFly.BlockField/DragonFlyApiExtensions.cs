using DragonFly.BlockField;

namespace DragonFly;

/// <summary>
/// DragonFLyApiExtensions
/// </summary>
public static class DragonFLyApiExtensions
{
    public static BlockFieldManager BlockField(this IDragonFlyApi api)
    {
        return BlockFieldManager.Default;
    }
}
