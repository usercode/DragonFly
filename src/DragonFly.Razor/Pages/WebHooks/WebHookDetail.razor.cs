using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Core.WebHooks;
using DragonFly.Models;
using DragonFly.Razor.Shared.UI.Toolbars;
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

        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            if(IsNewEntity)
            {
                toolbarItems.AddCreateButton(this);
            }
            else
            {
                toolbarItems.AddRefreshButton(this);
                toolbarItems.AddUpdateButton(this);
                toolbarItems.AddDeleteButton(this);
            }           
        }

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

        protected override async Task CreateActionAsync()
        {
            await WebHookStore.CreateAsync(Entity);

            NavigationManager.NavigateTo($"webhook/{Entity.Id}");
        }

        protected override async Task UpdateActionAsync()
        {
            await WebHookStore.UpdateAsync(Entity);
        }
    }
}
