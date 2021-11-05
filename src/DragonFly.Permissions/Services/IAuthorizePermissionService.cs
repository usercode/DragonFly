using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions
{
    /// <summary>
    /// IAuthorizePermissionService
    /// </summary>
    public interface IAuthorizePermissionService
    {
        Task<bool> AuthorizeAsync(ClaimsPrincipal principal, string permission);
    }
}
