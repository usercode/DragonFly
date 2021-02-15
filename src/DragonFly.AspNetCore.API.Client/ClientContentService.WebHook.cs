using DragonFly.AspNetCore;
using DragonFly.AspNetCore.Exports;
using DragonFly.AspNetCore.Rest.Models.WebHooks;
using DragonFly.AspNetCore.REST.Models;
using DragonFly.Content.ContentParts;
using DragonFly.Contents.Assets;
using DragonFly.Contents.Content;
using DragonFly.ContentTypes;
using DragonFly.Core;
using DragonFly.Core.Queries;
using DragonFly.Core.WebHooks;
using DragonFly.Data;
using DragonFly.Data.Models;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DragonFly.Client
{
    /// <summary>
    /// ContentService
    /// </summary>
    public partial class ClientContentService : IWebHookStorage
    {

        public async Task<WebHook> GetAsync(Guid id)
        {
            var response = await Client.GetAsync($"api/webhook/{id}");

            var e = await response.Content.ParseJsonAsync<RestWebHook>();

            return e.FromRest();
        }

        public async Task CreateAsync(WebHook entity)
        {
            var response = await Client.PostAsJson($"api/webhook", entity);

            var result = await response.Content.ParseJsonAsync<ResourceCreated>();

            entity.Id = result.Id;
        }

        public async Task UpdateAsync(WebHook entity)
        {
            await Client.PutAsJson($"api/webhook", entity);
        }

        public async Task DeleteAsync(WebHook webHook)
        {
            await Client.DeleteAsync($"api/webhook/{webHook.Id}");
        }

        public async Task<IEnumerable<WebHook>> QueryAsync(WebHookQuery query)
        {
            var response = await Client.GetAsync("api/webhook");

            var result = await response.Content.ParseJsonAsync<IEnumerable<RestWebHook>>();

            return result.Select(x => x.FromRest()).ToList();
        }
    }
}
