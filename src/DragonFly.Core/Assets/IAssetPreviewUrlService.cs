using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly;

public interface IAssetPreviewUrlService
{
    string CreateUrl(Asset asset, int width, int height);
}
