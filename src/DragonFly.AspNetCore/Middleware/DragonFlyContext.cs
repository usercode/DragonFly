// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.AspNet.Options;
using Microsoft.Extensions.Options;

namespace DragonFly.AspNetCore.Middleware;

public class DragonFlyContext
{
    public DragonFlyContext(IOptions<DragonFlyOptions> options)
    {
        Options = options.Value;
    }

    public DragonFlyOptions Options { get; }

}
