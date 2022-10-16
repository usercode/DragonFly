// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Razor.MainMenu;

namespace DragonFly;

public static class MenuItemManagerExtensions
{
    public static MenuItemManager MainMenu(this IDragonFlyApi api)
    {
        return MenuItemManager.Default;
    }
}
