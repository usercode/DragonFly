using DragonFly.Contents.Assets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Assets
{
    public interface IAssetProcessing
    {
        IEnumerable<string> MimeTypes { get; }

        Task OnAssetChangedAsync(Asset asset, Stream stream);

    }
}
