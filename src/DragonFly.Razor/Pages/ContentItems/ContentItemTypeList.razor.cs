using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages.ContentItems;

public class ContentItemTypeListBase : ContentSchemaListBase
{
    public ContentItemTypeListBase()
    {

    }

    protected override string GetNavigationPath(ContentSchema entity)
    {
        return $"content/{entity.Name}";
    }

}
