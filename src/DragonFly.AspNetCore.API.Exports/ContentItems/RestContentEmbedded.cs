using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Contents.Content;
using Newtonsoft.Json;

namespace DragonFly.Models
{
    /// <summary>
    /// RestContentEmbedded
    /// </summary>
    public class RestContentEmbedded
    {
        public RestContentEmbedded()
        {
            Fields = new RestContentFields();
        }

        /// <summary>
        /// Schema
        /// </summary>
        [JsonProperty("Schema", Order = 1)]
        public RestContentSchema Schema { get; set; }

        /// <summary>
        /// Fields
        /// </summary>
        [JsonProperty("Fields", Order = 30)]
        public RestContentFields Fields { get; set; }
    }
}
 