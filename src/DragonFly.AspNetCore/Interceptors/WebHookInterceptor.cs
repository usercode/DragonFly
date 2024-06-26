﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore;

/// <summary>
/// WebHookInterceptor
/// </summary>
public class WebHookInterceptor : IContentInterceptor
{
    public WebHookInterceptor(
        HttpClient httpClient,
        IBackgroundTaskManager backgroundTaskManager,
        IWebHookStorage webHookStorage,
        ILogger<WebHookInterceptor> logger)
    {
        HttpClient = httpClient;
        BackgroundTaskManager = backgroundTaskManager;
        WebHookStorage = webHookStorage;
        Logger = logger;
    }

    /// <summary>
    /// HttpClient
    /// </summary>
    public HttpClient HttpClient { get; }

    /// <summary>
    /// BackgroundTaskManager
    /// </summary>
    public IBackgroundTaskManager BackgroundTaskManager { get; }

    /// <summary>
    /// Api
    /// </summary>
    public IWebHookStorage WebHookStorage { get; }

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger<WebHookInterceptor> Logger { get; }

    public async Task OnPublishedAsync(ContentItem contentItem)
    {
        QueryResult<WebHook> result = await WebHookStorage.QueryAsync(new WebHookQuery());

        foreach (WebHook webHook in result.Items)
        {
            Logger.LogInformation("Starting webhook for {schema} with id {id}", contentItem.Schema.Name, contentItem.Id);

            if (webHook.TargetUrl == null)
            {
                continue;
            }

            QueryString query = QueryString.Create(new KeyValuePair<string, string?>[]
            {
                    new ("schema", contentItem.Schema.Name),
                    new ("id", contentItem.Id.ToString()),
                    new ("publish", "true")
            });

            Uri url = new Uri(webHook.TargetUrl + query);

            HttpContent content = new StringContent(string.Empty);

            foreach (HeaderItem header in webHook.Headers)
            {
                content.Headers.Add(header.Name, header.Value);
            }

            //send webhook
            HttpResponseMessage response = await HttpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation("Sent webhook for published content successfully: {schema}/{id}", contentItem.Schema.Name, contentItem.Id);
            }
            else
            {
                Logger.LogError("Failed to send webhook for published content: {schema}/{id} HTTP-Status:{status}", contentItem.Schema.Name, contentItem.Id, response.StatusCode);
            }
        }
    }

    public async Task OnPublishedAsync(Asset asset)
    {
        QueryResult<WebHook> result = await WebHookStorage.QueryAsync(new WebHookQuery());

        foreach (WebHook item in result.Items)
        {
            HttpResponseMessage response = await HttpClient.PostAsync($"{item.TargetUrl}?asset={asset.Id}", new StringContent(""));

            if (response.IsSuccessStatusCode == false)
            {
                Logger.LogInformation($"Sent webhook for published asset successfully.");
            }
            else
            {
                Logger.LogError("Failed to send webhook for published asset: {asset} HTTP-Status:{status}", asset.Id, response.StatusCode);
            }
        }
    }

    public Task OnPublishingAsync(ContentItem contentItem)
    {
        return Task.CompletedTask;
    }

    public async Task OnUnpublishedAsync(ContentItem contentItem)
    {
        QueryResult<WebHook> result = await WebHookStorage.QueryAsync(new WebHookQuery());

        foreach (WebHook item in result.Items)
        {
            Logger.LogInformation("Starting webhook for {schema} with id {id}", contentItem.Schema.Name, contentItem.Id);

            if (item.TargetUrl == null)
            {
                continue;
            }

            QueryString query = QueryString.Create(new KeyValuePair<string, string?>[]
            {
                new ("schema", contentItem.Schema.Name),
                new ("id", contentItem.Id.ToString()),
                new ("publish", "false")
            });

            Uri url = new Uri(item.TargetUrl + query);

            HttpResponseMessage response = await HttpClient.PostAsync(url, new StringContent(string.Empty));

            if (response.IsSuccessStatusCode)
            {
                Logger.LogInformation("Sent webhook for unpublished content successfully: {schema}/{id}", contentItem.Schema.Name, contentItem.Id);
            }
            else
            {
                Logger.LogError("Failed to send webhook for unpublished content: {schema}/{id} HTTP-Status:{status}", contentItem.Schema.Name, contentItem.Id, response.StatusCode);
            }
        }
    }

    public Task OnCreatedAsync(ContentItem contentItem)
    {
        return Task.CompletedTask;
    }

    public Task OnUpdatedAsync(ContentItem contentItem)
    {
        return Task.CompletedTask;
    }

    public Task OnDeletedAsync(ContentItem contentItem)
    {
        return Task.CompletedTask;
    }
}
