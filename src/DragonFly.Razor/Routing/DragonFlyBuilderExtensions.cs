using DragonFly.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor;

public static class DragonFlyBuilderExtensions
{
    public static IDragonFlyBuilder AddRazorRouting(this IDragonFlyBuilder builder)
    {
        RazorRoutingManager.Default.Items.Add(Assembly.GetCallingAssembly());

        return builder;
    }
}
