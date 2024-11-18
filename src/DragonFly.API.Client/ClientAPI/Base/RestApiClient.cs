// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API.Client;

/// <summary>
/// ApiClient
/// </summary>
public class RestApiClient
{
    public RestApiClient(HttpClient httpClient)
    {
        Http = httpClient;
    }

    /// <summary>
    /// Http
    /// </summary>
    public HttpClient Http { get; }
}
