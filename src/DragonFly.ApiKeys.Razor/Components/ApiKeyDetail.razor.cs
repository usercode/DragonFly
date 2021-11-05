using DragonFly.Client.Base;
using DragonFly.Permissions.Services;
using DragonFly.Razor.Helpers;
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
    public class ApiKeyDetailBase : EntityDetailComponent<ApiKey>
    {
        public ApiKeyDetailBase()
        {

        }

        [Inject]
        public IApiKeyService ApiKeyService { get; set; }

        [Inject]
        public IPermissionService PermissionService { get; set; }

        public IEnumerable<SelectableElementTree<Permission>> Permissions { get; set; }

        protected override void BuildToolbarItems(IList<ToolbarItem> toolbarItems)
        {
            base.BuildToolbarItems(toolbarItems);

            if (IsNewEntity)
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
            IEnumerable<Permission> permissions = await PermissionService.GetPermissionsAsync();

            Entity = await ApiKeyService.GetApiKey(EntityId);
        }

        protected override async Task UpdateActionAsync()
        {
            
        }
    }
}
