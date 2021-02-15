using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DragonFly.Contents.Content
{
    public abstract class RestContentBase
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("Id", Order = 0)]
        public Guid Id { get; set; }

        [JsonProperty("CreatedAt", Order = 10)]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("ModifiedAt", Order = 11)]
        public DateTime? ModifiedAt { get; set; }

        [JsonProperty("PublishedAr", Order = 12)]
        public DateTime? PublishedAt { get; set; }

        [JsonProperty("Version", Order = 12)]
        public int Version { get; set; }
    }
}
