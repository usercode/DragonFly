using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.BlockField;
public static class BlockFieldManagerExtensions
{
    public static void AddDefaults(this BlockFieldManager manager)
    {
        manager.Add<ColumnBlock>();
        manager.Add<AssetBlock>();
        manager.Add<TextBlock>();
        manager.Add<HtmlBlock>();
        manager.Add<YouTubeBlock>();        
        manager.Add<CodeBlock>();
        manager.Add<OpenGraphBlock>();
        manager.Add<HeadingBlock>();
        manager.Add<QuoteBlock>();
        manager.Add<ReferenceBlock>();
        manager.Add<ContainerBlock>();

        manager.Add<UnknownBlock>();
    }
}
