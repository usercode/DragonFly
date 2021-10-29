using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity
{
    public class IdentityRole : Entity
    {
        public IdentityRole()
        {
            Permissions = new List<IdentityPermission>();
        }

        public string Name { get; set; }

        public IList<IdentityPermission> Permissions { get; set; }
    }
}
