using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Assets
{
    [AssetMetadata("Image")]
    public class ImageMetadata : AssetMetadata
    {
        /// <summary>
        /// Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }
    }
}
