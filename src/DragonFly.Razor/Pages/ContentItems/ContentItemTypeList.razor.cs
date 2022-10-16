// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

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
