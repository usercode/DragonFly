﻿using BlazorStrap;
using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Razor.Base;
using DragonFly.Razor.Helpers;
using DragonFly.Razor.Shared.UI.Toolbars;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Components.Users
{
    public class UserDetailBase : EntityDetailComponent<IdentityUser>
    {
        public UserDetailBase()
        {

        }

        [Inject]
        public IIdentityService UserStore { get; set; }

        public IEnumerable<SelectableObject<IdentityRole>> Roles { get; set; }

        public string NewPassword { get; set; }

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
            Entity = await UserStore.GetUserAsync(EntityId);

            IEnumerable<IdentityRole> roles = await UserStore.GetRolesAsync();

            Roles = roles.Select(x => new SelectableObject<IdentityRole>(Entity.Roles.Any(r => r.Id == x.Id), x)).ToList();

            
        }

        protected override void OnSaving(SavingEventArgs args)
        {
            base.OnSaving(args);

            Entity.Roles = Roles.Where(x => x.IsSelected).Select(x => x.Object).ToList();
        }

        protected override async Task UpdateActionAsync()
        {
            await UserStore.UpdateUserAsync(Entity);

            if (string.IsNullOrEmpty(NewPassword) == false)
            {
                await UserStore.ChangePasswordAsync(Entity.Id, NewPassword);
            }
        }

    }
}
