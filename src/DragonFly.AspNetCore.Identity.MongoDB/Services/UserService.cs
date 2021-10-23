using DragonFly.Identity.Models;
using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Identity.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Services
{
    /// <summary>
    /// UserService
    /// </summary>
    class UserService : IUserStore
    {
        public UserService(UserManager<DbUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<DbUser> UserManager { get; }

        public async Task<IDragonFlyUser> GetUserAsync(string username)
        {
            var user = await UserManager.FindByNameAsync(username);

            return user;
        }

        public async Task<IList<IDragonFlyUser>> GetUsersAsync()
        {
            return UserManager.Users.ToList().Cast<IDragonFlyUser>().ToList();
        }
    }
}
