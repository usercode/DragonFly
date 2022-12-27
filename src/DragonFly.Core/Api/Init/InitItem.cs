// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.API;

class InitItem : IInitialize, IPreInitialize, IPostInitialize
{
    public InitItem(Action<IDragonFlyApi> action)
    {
        Action = action;
    }

    private Action<IDragonFlyApi> Action { get; }

    public async Task ExecuteAsync(IDragonFlyApi api)
    {
        Action(api);
    }
}
