﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client.Pages.Settings.Modules;

namespace DragonFly.Client;

/// <summary>
/// SettingsModule
/// </summary>
public class SettingsModule : ClientModule
{
    public override string Name => "Settings";

    public override string Description => "Manage settings";

    public override string Author => "DragonFly";

    public override void Init(IDragonFlyApi api)
    {
        api.MainMenu().Add("Settings", "fa-solid fa-gear", "settings");

        api.Settings().Add<ClientModules>("Modules");
    }
}