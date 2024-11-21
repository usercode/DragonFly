// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Http;

namespace DragonFly.ApiKeys;

public static class HttpClientExtensions
{
    /// <summary>
    /// Adds the API key to the default headers of the http client.
    /// </summary>
    public static void SetApiKey(this HttpClient client, string apiKey)
    {
        client.DefaultRequestHeaders.Add(ApiKeysConstants.ApiKeyHeaderName, apiKey);
    }
}
