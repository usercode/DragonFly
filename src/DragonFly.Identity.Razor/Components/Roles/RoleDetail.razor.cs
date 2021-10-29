using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Razor.Helpers;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public IEnumerable<SelectableObject<IdentityPermission>> Permissions { get; set; }

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

            //IEnumerable<IdentityPermission> roles = await UserStore.GetPermissionsAsync();

            //Permissions = roles.Select(x => new SelectableObject<IdentityPermission>(Entity.Permissions.Any(r => r.Id == x.Id), x)).ToList();


        }

        protected override async Task UpdateActionAsync()
        {
            await UserStore.UpdateRoleAsync(Entity);
        }
    }
}
