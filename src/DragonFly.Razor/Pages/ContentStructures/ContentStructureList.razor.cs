using DragonFly.Client.Base;
using DragonFly.Core.ContentStructures.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages;

public class ContentStructureListBase : EntityListComponent<ContentStructure>
{
    public ContentStructureListBase()
    {

    }

    protected override string GetNavigationPath(ContentStructure entity)
    {
        return $"structure/{entity.Name}";
    }

    protected override async Task RefreshActionAsync()
    {
        SearchResult = await ContentService.QueryAsync(new StructureQuery());
    }
}
