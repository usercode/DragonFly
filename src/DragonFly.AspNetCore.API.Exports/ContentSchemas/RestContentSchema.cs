using DragonFly.Contents.Content;
using DragonFly.Core.ContentItems.Queries;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.REST.Models
{
    public class RestContentSchema : RestContentBase
    {
        public RestContentSchema()
        {
            Fields = new Dictionary<string, RestContentFieldDefinition>();
            ListFields = new List<string>();
            ReferenceFields = new List<string>();
            OrderFields = new List<FieldOrder>();
        }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", Order = 1)]
        public string Name { get; set; }


        /// <summary>
        /// Parts
        /// </summary>
        [JsonProperty("Fields", Order = 10)]
        public IDictionary<string, RestContentFieldDefinition> Fields { get; set; }

        /// <summary>
        /// ListFields
        /// </summary>
        [JsonProperty("ListFields", Order = 21)]
        public IList<string> ListFields { get; set; }

        /// <summary>
        /// ReferenceFields
        /// </summary>
        [JsonProperty("ReferenceFields", Order = 22)]
        public IList<string> ReferenceFields { get; set; }

        /// <summary>
        /// OrderFields
        /// </summary>
        [JsonProperty("OrderFields", Order = 23)]
        public IList<FieldOrder> OrderFields { get; set; }
    }
}
