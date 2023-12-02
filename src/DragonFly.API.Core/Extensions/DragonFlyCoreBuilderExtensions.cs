// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.Builders;

namespace DragonFly.Core;

public static class DragonFlyCoreBuilderExtensions
{
    public static IDragonFlyBuilder AddRestApiCore(this IDragonFlyBuilder builder)
    {
        builder.Init(api =>
        {
            api.JsonFields().AddDefaults();
            api.ContentField().Added += fieldFactory => JsonFieldManager.Default.EnsureField(fieldFactory.FieldType);
        });

        return builder;
    }
}
