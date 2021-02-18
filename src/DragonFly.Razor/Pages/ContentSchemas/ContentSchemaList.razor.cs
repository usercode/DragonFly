using DragonFly.Client.Base;
using DragonFly.Content;
using DragonFly.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages
{
    public class ContentSchemaListBase : EntityListComponent<ContentSchema>
    {
        public ContentSchemaListBase()
        {

        }

        protected override string GetNavigationPath(ContentSchema entity)
        {
            return $"schema/{entity.Name}";
        }

        protected override async Task RefreshActionAsync()
        {
            SearchResult = await ContentService.GetContentSchemas();
        }
    }
}
