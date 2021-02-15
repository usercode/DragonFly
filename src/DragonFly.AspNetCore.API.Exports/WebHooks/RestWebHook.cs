using DragonFly.Contents.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.REST.Models
{
    /// <summary>
    /// RestWebHook
    /// </summary>
    public class RestWebHook : RestContentBase
    {
        public RestWebHook()
        {
            
        }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", Order = 1)]
        public string Name { get; set; }

        /// <summary>
        /// EventName
        /// </summary>
        [JsonProperty("EventName", Order = 2)]
        public virtual string EventName { get; set; }

        /// <summary>
        /// TargetUrl
        /// </summary>
        [JsonProperty("TargetUrl", Order = 3)]
        public virtual string TargetUrl { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("Description", Order = 4)]
        public virtual string Description { get; set; }
    }
}
