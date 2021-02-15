using DragonFly.Contents.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest.Models.Assets
{
    public class RestAssetFolder : RestContentBase
    {
        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", Order = 100)]
        public virtual string Name { get; set; }
    }
}
