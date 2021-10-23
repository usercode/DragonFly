using DragonFly.Identity.Models;
using DragonFly.Identity.Services;
using DragonFly.Razor.Pages.Settings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Components
{
    public partial class UserList
    {
        public UserList()
        {
            Users = new List<IDragonFlyUser>();
        }

        [Inject]
        public IUserStore UserStore { get; set; }

        public IList<IDragonFlyUser> Users { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Users = await UserStore.GetUsersAsync();
        }
    }
}
