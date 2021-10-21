using DragonFly.AspNetCore.Identity.MongoDB.Models;
using DragonFly.Security;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB
{
    /// <summary>
    /// LoginService
    /// </summary>
    class LoginService : ILoginService
    {
        public LoginService(SignInManager<DbUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public SignInManager<DbUser> SignInManager { get; }

        public async Task<bool> LoginAsync(string username, string password)
        {
            SignInResult result = await SignInManager.PasswordSignInAsync(username, password, false, true);

            return result.Succeeded;
        }

        public async Task Logout()
        {
            await SignInManager.SignOutAsync();
        }
    }
}
