using DragonFly.BlockField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
