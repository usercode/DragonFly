// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly;
using DragonFlyTemplate.Models;

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
        Result = await ContentStorage.QueryAsync<BlogPostModel>();

    }
}
