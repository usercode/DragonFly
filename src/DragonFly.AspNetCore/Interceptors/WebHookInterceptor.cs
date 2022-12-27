// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

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
        using (new DisablePermissions())
        {
            QueryResult<WebHook> result = await storage.QueryAsync(new WebHookQuery());

            foreach (WebHook item in result.Items)
            {
                Logger.LogInformation($"Starting webhook for {contentItem.Schema.Name} with id {contentItem.Id}");

                if (item.TargetUrl == null)
                {
                    continue;
                }

                QueryString query = QueryString.Create(new KeyValuePair<string, StringValues>[]
                {
                new ("schema", new StringValues(contentItem.Schema.Name)),
                new ("id", new StringValues(contentItem.Id.ToString())),
                new ("publish", new StringValues("true"))
                });

                Uri url = new Uri(item.TargetUrl + query);

                HttpResponseMessage response = await HttpClient.PostAsync(url, new StringContent(""));

                Logger.LogInformation($"Webhook send to {url} with status code {response.StatusCode}");

                string s = await response.Content.ReadAsStringAsync();
            }
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
        var result = await storage.QueryAsync(new WebHookQuery());

        foreach (WebHook item in result.Items)
        {
            Logger.LogInformation($"Starting webhook for {contentItem.Schema.Name} with id {contentItem.Id}");

            if (item.TargetUrl == null)
            {
                continue;
            }

            QueryString query = QueryString.Create(new KeyValuePair<string, StringValues>[]
            {
                new ("schema", contentItem.Schema.Name),
                new ("id", contentItem.Id.ToString()),
                new ("publish", "false")
            });

            Uri url = new Uri(item.TargetUrl + query);

            HttpResponseMessage response = await HttpClient.PostAsync(url, new StringContent(""));

            Logger.LogInformation($"Webhook send to {url} with status code {response.StatusCode}");

            string s = await response.Content.ReadAsStringAsync();
        }
    }

    public async Task OnUpdatedAsync(IDataStorage storage, ContentItem contentItem)
    {
        
    }
}
