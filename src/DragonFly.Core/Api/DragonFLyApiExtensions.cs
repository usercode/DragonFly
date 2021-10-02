using DragonFly.Content;
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
        public static ContentFieldManager ContentField(this IDragonFlyApi dragonFlyApi)
        {
            return ContentFieldManager.Default;
        }

        public static AssetMetadataManager AssetMetadata(this IDragonFlyApi dragonFlyApi)
        {
            return AssetMetadataManager.Default;
        }
    }
}
