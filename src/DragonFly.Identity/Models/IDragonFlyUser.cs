using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Models
{
    public interface IDragonFlyUser
    {
        string UserName { get; }

        string Email { get; }
    }
}
