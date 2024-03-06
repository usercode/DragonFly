// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.API;
using DragonFly.Builders;

namespace DragonFly.Core;

public static class DragonFlyCoreBuilderExtensions
{
    public static TDragonFlyBuilder AddRestApiCore<TDragonFlyBuilder>(this TDragonFlyBuilder builder)
        where TDragonFlyBuilder : IDragonFlyBuilder
    {
        builder.PreInit(api =>
        {
            api.ContentField().Added += factory => JsonFieldManager.Default.EnsureField(factory.FieldType);
        });

        builder.Init(api =>
        {
            api.JsonField().AddDefaults();
        });

        return builder;
    }
}
