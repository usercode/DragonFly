using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Content
{
    /// <summary>
    /// AssetFieldOptions
    /// </summary>
    public class AssetFieldOptions : ContentFieldOptions
    {
        public AssetFieldOptions()
        {
        }

        public override ContentField CreateContentField()
        {
            return new AssetField();
        }
    }
}
