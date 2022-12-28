// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

public static class MenuItemManagerExtensions
{
    public static MenuItemManager MainMenu(this IDragonFlyApi api)
    {
        return MenuItemManager.Default;
    }
}
