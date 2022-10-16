// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// IInitialize
/// </summary>
public interface IInitialize
{
    Task ExecuteAsync(IDragonFlyApi api);
}
