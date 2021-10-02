using DragonFly.Fields.BlockField.Storage.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

            byte[] buffer = Encoding.UTF8.GetBytes(json);

            MemoryStream mem = new MemoryStream();
            using (GZipStream brotli = new GZipStream(mem, CompressionLevel.Optimal))
            {
                new MemoryStream(buffer).CopyTo(brotli);
            }

            string result = Convert.ToBase64String(mem.ToArray());

            return result;
        }

        public static Document? Deserialize(string input)
        {
            if (input == null)
            {
                return new Document();
            }

            byte[] buffer = Convert.FromBase64String(input);

            MemoryStream mem = new MemoryStream();
            using (GZipStream brotli = new GZipStream(new MemoryStream(buffer), CompressionMode.Decompress))
            {
                brotli.CopyTo(mem);
            }

            string json = Encoding.UTF8.GetString(mem.ToArray());

            return JsonConvert.DeserializeObject<Document>(json, GetJsonSettings());
        }
    }
}
