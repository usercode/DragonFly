// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
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
        IWebHookStorage webHookStorage,
        ILogger<WebHookInterceptor> logger)
    {
        HttpClient = httpClient;
        WebHookStorage = webHookStorage;
        Logger = logger;
    }

    /// <summary>
    /// HttpClient
    /// </summary>
    public HttpClient HttpClient { get; }

    /// <summary>
    /// Api
    /// </summary>
    public IWebHookStorage WebHookStorage { get; }

    public ILogger<WebHookInterceptor> Logger { get; }

    public async Task OnPublishedAsync(ContentItem contentItem)
    {
        using (new DisablePermissions())
        {
            QueryResult<WebHook> result = await WebHookStorage.QueryAsync(new WebHookQuery());

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
            }
        }
    }

    public async Task OnPublishedAsync(Asset asset)
    {
        var result = await WebHookStorage.QueryAsync(new WebHookQuery());

        foreach (WebHook item in result.Items)
        {
            HttpResponseMessage response = await HttpClient.PostAsync($"{item.TargetUrl}?asset={asset.Id}", new StringContent(""));


        }
    }

    public async Task OnPublishingAsync(ContentItem contentItem)
    {

    }

    public async Task OnUnpublishedAsync(ContentItem contentItem)
    {
        var result = await WebHookStorage.QueryAsync(new WebHookQuery());

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
        }
    }

    public async Task OnCreatedAsync(ContentItem contentItem)
    {

    }

    public async Task OnUpdatedAsync(ContentItem contentItem)
    {

    }

    public async Task OnDeletedAsync(ContentItem contentItem)
    {

    }
}
