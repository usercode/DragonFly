using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.REST.Models;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Content;
using DragonFly.ContentTypes;
using Newtonsoft.Json;

namespace DragonFly.Models
{
    /// <summary>
    /// ContentItem
    /// </summary>
    public class RestContentItem : RestContentBase
    {
        public RestContentItem()
        {
            Fields = new RestContentFields(); 
        }

        /// <summary>
        /// Schema
        /// </summary>
        [JsonProperty("Schema", Order = 1)]
        public RestContentSchema Schema { get; set; }

        /// <summary>
        /// SchemaVersion
        /// </summary>
        [JsonProperty("SchemaVersion", Order = 2)]
        public int SchemaVersion { get; set; }

        /// <summary>
        /// Fields
        /// </summary>
        [JsonProperty("Fields", Order = 30)]
        public RestContentFields Fields { get; set; }
    }
}
 