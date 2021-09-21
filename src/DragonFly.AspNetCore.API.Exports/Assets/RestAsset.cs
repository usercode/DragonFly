using DragonFly.Contents.Content;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Models.Assets
{
    /// <summary>
    /// RestAsset
    /// </summary>
    public class RestAsset : RestContentBase
    {
        public RestAsset()
        {
            _metaddata = new Dictionary<string, JObject>();
        }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("Name", Order = 100)]
        public virtual string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("Alt", Order = 100)]
        public virtual string Alt { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [JsonProperty("Description", Order = 100)]
        public virtual string Description { get; set; }

        /// <summary>
        /// Filename
        /// </summary>
        [JsonProperty("Slug", Order = 100)]
        public virtual string Slug { get; set; }

        /// <summary>
        /// ContentType
        /// </summary>
        [JsonProperty("MimeType", Order = 100)]
        public virtual string MimeType { get; set; }

        /// <summary>
        /// Hash
        /// </summary>
        [JsonProperty("Hash", Order = 100)]
        public virtual string Hash { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        [JsonProperty("Size", Order = 100)]
        public virtual long? Size { get; set; }

        /// <summary>
        /// Folder
        /// </summary>
        [JsonProperty("Folder", Order = 100)]
        public virtual RestAssetFolder Folder { get; set; }

        private IDictionary<string, JObject> _metaddata;

        /// <summary>
        /// Metaddata
        /// </summary>
        [JsonProperty("Metadata", Order = 100)]
        public virtual IDictionary<string, JObject> Metaddata { get => _metaddata; set => _metaddata = value; }
    }
}
