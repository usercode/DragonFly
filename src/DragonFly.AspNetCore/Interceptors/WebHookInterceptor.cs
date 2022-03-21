using DragonFly.Content;
using DragonFly.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.WebHooks;

/// <summary>
/// WebHookInterceptor
/// </summary>
public class WebHookInterceptor : IContentInterceptor
{
    public WebHookInterceptor(
        HttpClient httpClient,
        ILogger<WebHookInterceptor> logger)
    {
        HttpClient = httpClient;
        Logger = logger;
    }

    /// <summary>
    /// HttpClient
    /// </summary>
    public HttpClient HttpClient { get; }

    public ILogger<WebHookInterceptor> Logger { get; }

    public async Task OnDeletedAsync(IDataStorage storage, ContentItem contentItem)
    {
        
    }

    public async Task OnPublishedAsync(IDataStorage storage, ContentItem contentItem)
    {
        var result = await storage.QueryAsync(new WebHookQuery());

        foreach (WebHook item in result.Items)
        {
            Logger.LogInformation($"Starting webhook for {contentItem.Schema.Name} with id {contentItem.Id}");

            string url = $"{item.TargetUrl}?schema={contentItem.Schema.Name}&id={contentItem.Id}";

            HttpResponseMessage response = await HttpClient.PostAsync(url, new StringContent(""));

            Logger.LogInformation($"Webhook send to {url} with status code {response.StatusCode}");

            string s = await response.Content.ReadAsStringAsync();
        }
    }

    public async Task OnPublishedAsync(IDataStorage storage, Asset asset)
    {
        var result = await storage.QueryAsync(new WebHookQuery());

        foreach (WebHook item in result.Items)
        {
            HttpResponseMessage response = await HttpClient.PostAsync($"{item.TargetUrl}?asset={asset.Id}", new StringContent(""));


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
