using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Contents.Assets
{
    public static class AssetDataUrlService
    {
        public static string GetDataUrl(this Asset asset)
        {
            return $"/dragonfly/api/asset/{asset.Id}/download?v={asset.Hash}";
        }

        public static string GetFileSize(this Asset asset)
        {
            if (asset.Size == null)
            {
                return "? KB";
            }

            return $"{(double)asset.Size / 1024:###,###,##0.00} KB";
        }
    }
}
