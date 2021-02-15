using DragonFly.Contents.Assets;
using DragonFly.Core.Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Assets
{
    public static class AssetDataUrlService
    {
        public static string GetDataUrl(this Asset asset)
        {
            return $"/dragonfly/api/asset/{asset.Id}/download?v={asset.Hash}";
        }
    }
}
