using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Rest.Commands
{
    public class CreateUser
    {
        public IdentityUser? User { get; set; }  

        public string? Password { get; set; }
    }
}
