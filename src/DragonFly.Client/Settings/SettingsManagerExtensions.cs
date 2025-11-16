// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

public static class SettingsManagerExtensions
{
    extension(IDragonFlyApi api)
    {
        public SettingsManager Settings => SettingsManager.Default;
    }
}
