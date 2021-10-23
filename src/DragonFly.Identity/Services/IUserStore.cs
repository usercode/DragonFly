using DragonFly.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Services
{
    public interface IUserStore
    {
        Task<IDragonFlyUser> GetUserAsync(string username);

        Task<IList<IDragonFlyUser>> GetUsersAsync();
    }
}
