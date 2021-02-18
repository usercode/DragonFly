using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Core.WebHooks;
using DragonFly.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems
{
    public class WebHookListBase : EntityListComponent<WebHook>
    {
        public WebHookListBase()
        {
        }

        /// <summary>
        /// WebHookStore
        /// </summary>
        [Inject]
        private IWebHookStorage WebHookStore { get; set; }

        protected override string GetNavigationPath(WebHook entity)
        {
            return $"webhook/{entity.Id}";
        }

        protected override async Task RefreshActionAsync()
        {
          
            //SearchResult = await ContentService.QueryAsync(new WebHookQuery());
        }
    }
}
