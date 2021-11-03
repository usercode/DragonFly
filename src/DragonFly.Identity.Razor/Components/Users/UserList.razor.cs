using DragonFly.Client.Base;
using DragonFly.Identity.Services;
using DragonFly.Razor.Pages.Settings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Components.Users
{
    public class UserListBase : EntityListComponent<IdentityUser>
    {
        public UserListBase()
        {
            Users = new List<IdentityUser>();
        }

        [Inject]
        public IIdentityService UserStore { get; set; }

        public IList<IdentityUser> Users { get; set; }

        protected override string GetNavigationPath(IdentityUser entity)
        {
            return $"settings/identity/user/{entity.Id}";
        }

        protected override async Task RefreshActionAsync()
        {
            Users = (await UserStore.GetUsersAsync()).ToList();
        }
    }
}
