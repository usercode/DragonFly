using DragonFly.Content;
using DragonFly.Core.WebHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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

        return mongoWebHook;
    }
}
