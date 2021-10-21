using DragonFly.Assets;
using DragonFly.Content;
using DragonFly.Fields.BlockField;
using DragonFly.Fields.BlockField.Razor;
using DragonFly.Fields.BlockField.Razor.Base;
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
    /// DragonFlyApiExtensions
    /// </summary>
    public static class DragonFlyApiExtensions
    {
        public static void RegisterBlock<TBlock, TBlockView>(this IDragonFlyApi api)
            where TBlock : Block
            where TBlockView : BlockComponent<TBlock>
        {
            api.RegisterBlock<TBlock>();
            api.Component().RegisterBlock<TBlockView>();
        }
    }
}
