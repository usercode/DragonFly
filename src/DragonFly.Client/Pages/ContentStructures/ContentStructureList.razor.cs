﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Base;
using DragonFly.Query;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DragonFly.Client.Pages;

public class ContentStructureListBase : EntityListComponent<ContentStructure>
{
    public ContentStructureListBase()
    {

    }


    [Inject]
    public IStructureStorage ContentService { get; set; }

    protected override string GetNavigationPath(ContentStructure entity)
    {
        return $"structure/{entity.Name}";
    }

    protected override async Task RefreshActionAsync()
    {
        SearchResult = await ContentService.QueryAsync(new StructureQuery());
    }
}
