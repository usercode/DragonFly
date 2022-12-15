﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using DragonFly.Query;
using DragonFly.Proxy;

namespace DragonFlyTemplate.Pages;

public class ProjectsPage : BasePageModel
{
    public ProjectsPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;

        PageTitle = "Projekte";
    }

    public IContentStorage ContentStorage { get; }

    public QueryResult<ProjectModel> Result { get; private set; }

    public async Task OnGetAsync()
    {
        Result = await ContentStorage.QueryAsync<ProjectModel>();
    }
}
