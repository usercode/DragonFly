// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.Extensions.DependencyInjection;

namespace DragonFly.Builders;

public interface IDragonFlyBuilder
{
    IServiceCollection Services { get; }

}
