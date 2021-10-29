using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity
{
    public class IdentityPermission : Entity
    {
        public IdentityPermission()
        {
            Name = string.Empty;
            Value = string.Empty;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
