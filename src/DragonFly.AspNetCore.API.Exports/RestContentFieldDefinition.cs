using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.AspNetCore.REST.Models
{
    public class RestContentFieldDefinition
    {
        /// <summary>
        /// Label
        /// </summary>
        [JsonProperty("Label", Order = 1)]
        public string Label { get; set; }

        /// <summary>
        /// SortKey
        /// </summary>
        [JsonProperty("SortKey", Order = 2)]
        public int SortKey { get; set; }

        /// <summary>
        /// FieldType
        /// </summary>
        [JsonProperty("FieldType", Order = 3)]
        public string FieldType { get; set; }

        /// <summary>
        /// Options
        /// </summary>
        [JsonProperty("Options", Order = 4)]
        public JObject Options { get; set; }
    }
}
