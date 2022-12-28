// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

public static class SettingsManagerExtensions
{
    public static SettingsManager Settings(this IDragonFlyApi api)
    {
        return SettingsManager.Default;
    }       
}
