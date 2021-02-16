using DragonFly.Client.Base;
using DragonFly.Core.Queries;
using DragonFly.Core.WebHooks;
using DragonFly.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems
{
    public class WebHookDetailBase : EntityDetailComponent<WebHook>
    {
        public WebHookDetailBase()
        {

        }

        /// <summary>
        /// WebHookStore
        /// </summary>
        [Inject]
        private IWebHookStorage WebHookStore { get; set; }

        protected override async Task RefreshActionAsync()
        {
            if (IsNewEntity)
            {
                Entity = new WebHook();
            }
            else
            {
                Entity = await WebHookStore.GetAsync(EntityId);
            }

        }

        protected override async Task SaveActionAsync()
        {
            if(IsNewEntity)
            {
                await WebHookStore.CreateAsync(Entity);

                NavigationManager.NavigateTo($"webhook/{Entity.Id}");
            }
            else
            {
                await WebHookStore.UpdateAsync(Entity);
            }

        }
    }
}
