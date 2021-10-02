using DragonFly.Content;
using DragonFly.Fields.BlockField;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    /// <summary>
    /// DragonFLyApiExtensions
    /// </summary>
    public static class DragonFLyApiExtensions
    {
        public static BlockFieldManager BlockField(this IDragonFlyApi dragonFlyApi)
        {
            return BlockFieldManager.Default;
        }
    }
}
