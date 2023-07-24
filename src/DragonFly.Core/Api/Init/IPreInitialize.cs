// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Init;

/// <summary>
/// IPreInitialize
/// </summary>
public interface IPreInitialize
{
    Task ExecuteAsync(IDragonFlyApi api);
}
