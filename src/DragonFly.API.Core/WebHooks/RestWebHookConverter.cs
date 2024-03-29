﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

public static class RestWebHookConverter
{
    public static RestWebHook ToRest(this WebHook webHook)
    {
        RestWebHook restWebHook = new RestWebHook();
        restWebHook.Id = webHook.Id;
        restWebHook.CreatedAt = webHook.CreatedAt;
        restWebHook.ModifiedAt = webHook.ModifiedAt;
        restWebHook.Version = webHook.Version;
        restWebHook.TargetUrl = webHook.TargetUrl;
        restWebHook.Name = webHook.Name;
        restWebHook.EventName = webHook.EventName;
        restWebHook.Headers = webHook.Headers;

        return restWebHook;
    }

    public static WebHook ToModel(this RestWebHook restWebHook)
    {
        WebHook webHook = new WebHook();
        webHook.Id = restWebHook.Id;
        webHook.CreatedAt = restWebHook.CreatedAt;
        webHook.ModifiedAt = restWebHook.ModifiedAt;
        webHook.Version = restWebHook.Version;
        webHook.TargetUrl = restWebHook.TargetUrl;
        webHook.Name = restWebHook.Name;
        webHook.EventName = restWebHook.EventName;
        webHook.Headers = restWebHook.Headers;

        return webHook;
    }
}
