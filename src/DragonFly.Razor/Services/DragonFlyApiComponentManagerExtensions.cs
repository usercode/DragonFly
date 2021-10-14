using DragonFly.Assets;
using DragonFly.Content;
using DragonFly.Razor.Pages.ContentItems.Fields;
using DragonFly.Razor.Pages.ContentItems.Query;
using DragonFly.Razor.Pages.ContentSchemas.Fields;
using DragonFly.Razor.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    /// <summary>
    /// DragonFlyApiComponentManagerExtensions
    /// </summary>
    public static class DragonFlyApiComponentManagerExtensions
    {
        public static ComponentManager Component(this IDragonFlyApi api)
        {
            return ComponentManager.Default;
        }
    }
}
