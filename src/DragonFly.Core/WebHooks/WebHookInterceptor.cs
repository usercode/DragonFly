using DragonFly.Contents.Assets;
using DragonFly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.WebHooks
{
    /// <summary>
    /// WebHookInterceptor
    /// </summary>
    public class WebHookInterceptor : IContentInterceptor
    {
        public WebHookInterceptor(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        /// HttpClient
        /// </summary>
        public HttpClient HttpClient { get; }

        public async Task OnDeletedAsync(IDataStorage storage, ContentItem contentItem)
        {
            
        }

        public async Task OnPublishedAsync(IDataStorage storage, ContentItem contentItem)
        {
            var result = await storage.QueryAsync(new WebHookQuery() { Event = null });

            foreach(WebHook item in result)
            {
                HttpResponseMessage response = await HttpClient.PostAsync(item.TargetUrl + $"?schema={contentItem.Schema.Name}&id={contentItem.Id}", new StringContent(""));

                string s = await response.Content.ReadAsStringAsync();

            }
        }

        public async Task OnPublishedAsync(IDataStorage storage, Asset asset)
        {
            var result = await storage.QueryAsync(new WebHookQuery() { Event = null });

            foreach (WebHook item in result)
            {
                HttpResponseMessage response = await HttpClient.PostAsync(item.TargetUrl + $"?asset={asset.Id}", new StringContent(""));


            }
        }

        public async Task OnPublishingAsync(IDataStorage storage, ContentItem contentItem)
        {

        }

        public async Task OnUnpublishedAsync(IDataStorage storage, ContentItem contentItem)
        {
            
        }

        public async Task OnUpdatedAsync(IDataStorage storage, ContentItem contentItem)
        {
            
        }
    }
}
