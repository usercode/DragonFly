// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;

namespace DragonFly.Client;

/// <summary>
/// DragonFlyApp
/// </summary>
public class DragonFlyApp
{
    public DragonFlyApp(Uri apiBaseUrl, Uri clientBaseUrl)
    {
        ApiBaseUrl = apiBaseUrl;
        ClientBaseUrl = clientBaseUrl;
    }

    /// <summary>
    /// ApiBaseUrl
    /// </summary>
    public Uri ApiBaseUrl { get; }

    /// <summary>
    /// ClientBaseUrl
    /// </summary>
    public Uri ClientBaseUrl { get; }
}
