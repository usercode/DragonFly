using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore.Identity.MongoDB.Models
{
    class IdentityPermissionItem
    {
        public IdentityPermissionItem()
        {
            Name = string.Empty;
            Value = string.Empty;
        }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
