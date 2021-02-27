using DragonFly.Content;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Assets
{
    public interface IAssetMetadataComponent
    {

        AssetMetadata Metadata { get; }
    }
}
