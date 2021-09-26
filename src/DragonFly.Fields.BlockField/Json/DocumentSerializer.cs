using DragonFly.Fields.BlockField.Storage.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Storage.Serializers
{
    /// <summary>
    /// DocumentSerializer
    /// </summary>
    public class DocumentSerializer
    {
        private static JsonSerializerSettings GetJsonSettings()
        {
            return new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new JsonDiscriminatorBinder()
            };
        }

        public static string Serialize(Document? document)
        {
            string json = JsonConvert.SerializeObject(document, GetJsonSettings());

            return json;
        }

        public static Document? Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Document>(json, GetJsonSettings());
        }
    }
}
