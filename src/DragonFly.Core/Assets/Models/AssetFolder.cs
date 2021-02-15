using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Assets
{
    public class AssetFolder : ContentBase
    {
        public virtual string Name { get; set; }

        public virtual AssetFolder Parent { get; set; }
    }
}
