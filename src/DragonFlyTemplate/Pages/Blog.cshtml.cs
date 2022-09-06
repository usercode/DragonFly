﻿using DragonFly;
using DragonFly.AspNetCore.API.Exports;
using DragonFly.Content;
using DragonFly.Storage;
using DragonFlyTemplate.Models;
using DragonFly.AspNetCore.SchemaBuilder;
using DragonFly.Query;

namespace DragonFlyTemplate.Pages;

public class BlogPage : BasePageModel
{
    public BlogPage(IContentStorage contentStorage)
    {
        ContentStorage = contentStorage;

        PageTitle = "Blog";
    }

    public IContentStorage ContentStorage { get; }

    public QueryResult<BlogPostModel> Result { get; private set; }

    public async Task OnGetAsync()
    {
        Result = await ContentStorage.QueryAsync<BlogPostModel>(new ContentItemQuery() { Top = 100, Skip = 0, Published = true });


    }
}
