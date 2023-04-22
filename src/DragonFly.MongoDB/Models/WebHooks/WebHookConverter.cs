// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.MongoDB;

static class WebHookConverter
{
    public static WebHook ToModel(this MongoWebHook mongoWebHook)
    {
        WebHook webHook = new WebHook();
        webHook.Id = mongoWebHook.Id;
        webHook.Version = mongoWebHook.Version;
        webHook.CreatedAt = mongoWebHook.CreatedAt;
        webHook.ModifiedAt = mongoWebHook.ModifiedAt;
        webHook.Name = mongoWebHook.Name;
        webHook.Description = mongoWebHook.Description;
        webHook.TargetUrl = mongoWebHook.TargetUrl;
        webHook.EventName = mongoWebHook.EventName;
        webHook.Headers = mongoWebHook.Headers;

        return webHook;
    }

    public static MongoWebHook ToMongo(this WebHook webHook)
    {
        MongoWebHook mongoWebHook = new MongoWebHook();
        mongoWebHook.Id = webHook.Id;
        mongoWebHook.Version = webHook.Version;
        mongoWebHook.CreatedAt = webHook.CreatedAt;
        mongoWebHook.ModifiedAt = webHook.ModifiedAt;
        mongoWebHook.Name = webHook.Name;
        mongoWebHook.Description = webHook.Description;
        mongoWebHook.TargetUrl = webHook.TargetUrl;
        mongoWebHook.EventName = webHook.EventName;
        mongoWebHook.Headers = webHook.Headers;

        return mongoWebHook;
    }
}
