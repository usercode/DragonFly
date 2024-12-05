// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client.Pages.ContentItems;

public partial class ContentItemTypeList
{
    protected override string GetNavigationPath(ContentSchema entity)
    {
        return $"content/{entity.Name}";
    }

}
