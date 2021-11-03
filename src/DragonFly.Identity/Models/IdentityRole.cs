using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity
{
    /// <summary>
    /// IdentityRole
    /// </summary>
    public class IdentityRole : Entity
    {
        public IdentityRole()
        {
            Name = string.Empty;
            Permissions = new List<string>();
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Permissions
        /// </summary>
        public IList<string> Permissions { get; set; }
    }
}
