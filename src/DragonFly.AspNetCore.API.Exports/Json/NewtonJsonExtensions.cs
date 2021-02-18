using DragonFly.AspNetCore;
using DragonFly.Content;
using DragonFly.Data;
using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Client
{
    public static class NewtonJsonExtensions
    {
        public static JsonSerializer CreateSerializer()
        {
            var d = new FieldTypeDiscriminatorMapper();
            foreach (var t in ContentFieldManager.Default.GetAllOptionTypes())
            {
                d.AddType(t);
            }

            return JsonSerializer.Create(new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new TypeNameSerializationBinder(d),
                Formatting = Formatting.Indented
            });
        }

        public static T Deserialize<T>(string json)
        {
            var s = CreateSerializer();

            return s.Deserialize<T>(new JsonTextReader(new StringReader(json)));
        }

        public static string Serialize<T>(T obj)
        {
            var s = CreateSerializer();

            StringBuilder builder = new StringBuilder();
            JsonTextWriter writer = new JsonTextWriter(new StringWriter(builder));

            s.Serialize(writer, obj);

            return builder.ToString();
        }

        public static async Task<T> ParseJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();

            return Deserialize<T>(json);
        }

        public static async Task PutAsJson<T>(this HttpClient client, string url, T entity)
        {
            string json = Serialize(entity);

            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

        public static async Task<HttpResponseMessage> PostAsJson<T>(this HttpClient client, string url, T entity)
        {
            string json = Serialize(entity);

            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return response;
        }

    }
}
