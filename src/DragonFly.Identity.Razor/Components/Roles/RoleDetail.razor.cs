using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Permissions;
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

            HttpResponseMessage response = await Client.PostAsync("permission/query", new StringContent(string.Empty));

            response.EnsureSuccessStatusCode();

            IEnumerable<Permission>? permissions = await response.Content.ReadFromJsonAsync<IEnumerable<Permission>>();

            if (permissions == null)
            {
                throw new Exception("Could not load the permissions.");
            }

            IEnumerable<SelectableElementTree<Permission>> Transform(IEnumerable<Permission> permissions)
            {
                foreach (Permission permission in permissions
                                                            .OrderBy(x => x.SortKey)
                                                            .ThenBy(x => x.Name))
                {
                    yield return new SelectableElementTree<Permission>(
                                            Entity.Permissions.Any(p => p == permission.Name),
                                            permission,
                                            Transform(permission.Childs).ToList())
                            .EnableActivePath();
                }
            };

            Permissions = Transform(permissions).ToList();
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
