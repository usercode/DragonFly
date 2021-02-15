using DragonFly.Content.ContentParts;
using DragonFly.Contents.Assets;
using DragonFly.Contents.Content.Fields;
using DragonFly.Core.ContentItems.Models.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content.Parts.Base
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
