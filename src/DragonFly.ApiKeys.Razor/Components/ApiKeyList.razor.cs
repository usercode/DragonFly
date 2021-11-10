using DragonFly.Client.Base;
using DragonFly.Razor.Shared.UI.Toolbars;
using DragonFLy.ApiKeys;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.ApiKeys.Razor.Components
{
    public class ApiKeyListBase : EntityListComponent<ApiKey>
    {
        public ApiKeyListBase()
        {
            Items = new List<ApiKey>();
        }

        [Inject]
        public IApiKeyService ApiKeyService { get; set; }

        public IEnumerable<ApiKey> Items { get; set; }    

        protected override string GetNavigationPath(ApiKey entity)
        {
            return $"settings/apikey/{entity.Id}";
        }

        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            toolbarItems.Add(new ToolbarItem("Create", BlazorStrap.Color.Success, async () => Navigation.NavigateTo("settings/apikey/create")));
            toolbarItems.AddRefreshButton(this);
        }

        protected override async Task RefreshActionAsync()
        {
            Items = await ApiKeyService.GetAllApiKeys();
        }
    }
}
