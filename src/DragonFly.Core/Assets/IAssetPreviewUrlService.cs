using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client.Core.Assets;

public interface IAssetPreviewUrlService
{
    string CreateImageUrl(Asset asset, int width, int height);
}
