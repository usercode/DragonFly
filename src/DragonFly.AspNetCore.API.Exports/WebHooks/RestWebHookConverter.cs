using DragonFly.AspNetCore.API.Models;
using DragonFly.Content;
using DragonFly.Core.WebHooks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.API.Models.WebHooks
{
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

            return restWebHook;
        }

        public static WebHook FromRest(this RestWebHook restWebHook)
        {
            WebHook webHook = new WebHook();
            webHook.Id = restWebHook.Id;
            webHook.CreatedAt = restWebHook.CreatedAt;
            webHook.ModifiedAt = restWebHook.ModifiedAt;
            webHook.Version = restWebHook.Version;
            webHook.TargetUrl = restWebHook.TargetUrl;
            webHook.Name = restWebHook.Name;
            webHook.EventName = restWebHook.EventName;

            return webHook;
        }
    }
}
