using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    public class AssetFolder : ContentBase
    {
        public AssetFolder()
        {

        }

        public AssetFolder(Guid id)
        {
            Id = id;
        }


        public virtual string Name { get; set; }

        public virtual AssetFolder Parent { get; set; }
    }
}
