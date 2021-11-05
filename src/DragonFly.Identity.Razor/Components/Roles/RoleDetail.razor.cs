using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Permissions;
using DragonFly.Permissions.Razor;
using DragonFly.Permissions.Services;
using DragonFly.Razor.Helpers;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Components.Roles
{
    public class RoleDetailBase : EntityDetailComponent<IdentityRole>
    {
        public RoleDetailBase()
        {

        }

        [Inject]
        public IIdentityService UserStore { get; set; }

        [Inject]
        public HttpClient Client { get; set; }

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
            Entity = await UserStore.GetRoleAsync(EntityId);

            IEnumerable<Permission> permissions = await PermissionService.GetPermissionsAsync();

            Permissions = permissions
                                    .ToSelectableStructure(x => Entity.Permissions.Any(p => p == x.Name))
                                    .ToList();
        }

        protected override async Task UpdateActionAsync()
        {
            Entity.Permissions = Permissions
                                        .ToFlatList()
                                        .Select(x => x.Name)
                                        .ToList();

            await UserStore.UpdateRoleAsync(Entity);
        }
    }
}
