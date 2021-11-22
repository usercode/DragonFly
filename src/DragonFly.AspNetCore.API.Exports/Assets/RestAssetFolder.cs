using DragonFly.Contents.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Models.Assets
{
    public class RestAssetFolder : RestContentBase
    {
        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; set; }
    }
}
