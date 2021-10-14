using DragonFly.Assets;
using DragonFly.Razor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Modules
{
    /// <summary>
    /// AssetModule
    /// </summary>
    public class AssetModule : ClientModule
    {
        public override string Name => "AssetModule";

        public override string Description => "Manage assets for content items";

        public override string Author => "DragonFly";

        public override void Init(IDragonFlyApi api)
        {
            api.AddMenuItem("Assets", "far fa-image icon", "asset");

            api.RegisterMetadata<ImageMetadata, ImageMetadataView>();
            api.RegisterMetadata<PdfMetadata, PdfMetadataView>();
        }
    }
}
