using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Assets
{
    public interface IImageAssetUrlService
    {
        string Resize(Asset asset, int width, int height);

        string Pdf(Asset asset, int width, int height);
    }
}
