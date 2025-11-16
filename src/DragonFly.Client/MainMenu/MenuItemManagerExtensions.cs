// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

public static class MenuItemManagerExtensions
{
    extension(IDragonFlyApi api)
    {
        public MenuItemManager MainMenu => MenuItemManager.Default;
    }
}
