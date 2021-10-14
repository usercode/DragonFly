using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class DragonFlyApiContentFieldExtensions
    {
        public static void RegisterField<TContentField>(this IDragonFlyApi api)
            where TContentField : ContentField, new()
        {
            api.ContentField().RegisterField<TContentField>();
        }
    }
}
