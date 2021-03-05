using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Helpers
{
    public static class AssetExtensions
    {
        public static string ToFileSize(this Asset asset)
        {
            return  $"{(double)asset.Size / 1024:###,###,##0.00} KB";
        }
    }
}
