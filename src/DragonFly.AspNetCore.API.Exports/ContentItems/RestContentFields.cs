using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DragonFly.Content
{
    /// <summary>
    /// ContentField
    /// </summary>
    public class RestContentFields : Dictionary<string, JToken>
    {
        public RestContentFields()
        {

        }
    }
}
