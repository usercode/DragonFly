using DragonFly.Content.Queries;
using DragonFly.Contents.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.API.Models
{
    public class RestContentStructure : RestContentBase
    {
        public RestContentStructure()
        {
        }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", Order = 1)]
        public string Name { get; set; }


    }
}
