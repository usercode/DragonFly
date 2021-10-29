using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity
{
    public class IdentityUser : Entity
    {
        public IdentityUser()
        {
            Roles = new List<IdentityRole>();
        }

        public string UserName { get; set; }

        public string NormalizedUsername { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public IList<IdentityRole> Roles { get; set; }
    }
}
