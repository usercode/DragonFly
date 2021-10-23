using DragonFly.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Razor.Models
{
    public class DragonFlyUser : IDragonFlyUser
    {
        public DragonFlyUser()
        {
            UserName = string.Empty;
            Email = string.Empty;
        }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
