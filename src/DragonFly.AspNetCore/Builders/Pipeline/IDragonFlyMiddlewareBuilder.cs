﻿// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.AspNetCore.Builders;

public interface IDragonFlyMiddlewareBuilder
{
    void Endpoints(Action<IDragonFlyEndpointBuilder> endpoints);

    void Builder(Action<IDragonFlyApplicationBuilder> builder);
}
