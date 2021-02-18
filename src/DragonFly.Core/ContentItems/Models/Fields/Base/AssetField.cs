using DragonFly.Contents.Assets;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Content
{
    [FieldOptions(typeof(AssetFieldOptions))]
    public class AssetField : ContentField
    {
        public AssetField()
        {
        }

        public Asset Asset { get; set; }
    }
}
