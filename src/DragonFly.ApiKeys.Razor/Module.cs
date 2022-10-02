// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.ApiKeys.Razor.Components;
using DragonFly.Razor.Modules;

namespace DragonFly.ApiKeys.Razor;

public class Module : ClientModule
{
    public override string Name => "ApiKey";

    public override string Author => "DragonFly";

    public override void Init(IDragonFlyApi api)
    {
        api.Settings().Add<ApiKeyList>("ApiKey");
    }
}
