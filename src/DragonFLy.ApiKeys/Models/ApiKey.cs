using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFLy.ApiKeys
{
    /// <summary>
    /// ApiKey
    /// </summary>
    public class ApiKey : Entity
    {
        public ApiKey()
        {
            Name = string.Empty;
            Value = string.Empty;
            Permissions = new List<string>();
        }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Permissions
        /// </summary>
        public IList<string> Permissions { get; set; }
    }
}
