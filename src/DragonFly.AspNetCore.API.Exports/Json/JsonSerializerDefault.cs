using DragonFly.Core.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Exports.Json
{
    public class JsonSerializerDefault
    {
        public JsonSerializerDefault()
        {
        }

        private static JsonSerializerOptions? _options;

        public static JsonSerializerOptions Options
        {
            get
            {
                if (_options == null)
                {
                    _options = new JsonSerializerOptions();
                    _options.PropertyNameCaseInsensitive = true;
                    _options.WriteIndented = true;
                    _options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    _options.Converters.Add(new ArrayOptionJsonConverter());
                    _options.Converters.Add(new AssetMetadataJsonConverter());
                    _options.Converters.Add(new QueryFieldJsonConverter());
                }

                return _options;
            }
        }
    }
}
