// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Client;

/// <summary>
/// BackgroundTaskModule
/// </summary>
public class BackgroundTaskModule : ClientModule
{
    public override string Name => "Tasks";

    public override string Description => "Manage background tasks";

    public override string Author => "DragonFly";

    public override void Init(IDragonFlyApi api)
    {
        api.MainMenu().Add("Tasks", "fa-solid fa-layer-group", "tasks");

        
    }
}
