using DragonFly.Client;
using DragonFly.Contents.Content.Fields;
using DragonFly.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Rest
{
    class JsonService
    {
        public JsonService()
        {
        }


        public string Serialize<T>(T obj)
        {
            return NewtonJsonExtensions.Serialize<T>(obj);
        }

        public async Task<T> Deserialize<T>(Stream stream)
        {
            StreamReader reader = new StreamReader(stream);

            string json = await reader.ReadToEndAsync();

            return Deserialize<T>(json);
        }

        public T Deserialize<T>(string json)
        {
            return NewtonJsonExtensions.Deserialize<T>(json);
        }
    }
}
