// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DragonFly.Core;

public static class DragonFlyBuilderExtensions
{
    /// <summary>
    /// Adds DragonFly core services.
    /// <br /><br />
    /// Default fields: <br />
    /// <see cref="BoolField"/>, <see cref="StringField"/>, <see cref="SlugField"/>, <see cref="TextField"/>, <see cref="IntegerField"/>, <see cref="FloatField"/>, <see cref="HtmlField"/>, <see cref="ColorField"/>, <see cref="GeolocationField"/><br />
    /// <see cref="AssetField"/>, <see cref="ReferenceField"/>
    /// <br /><br />
    /// Default asset metadata: <br/>
    /// <see cref="ImageMetadata"/>, <see cref="PdfMetadata"/>, <see cref="VideoMetadata"/><br /><br />
    /// Default services::<br/>
    /// <see cref="ISlugService"/> -> <see cref="SlugService"/><br />
    /// </summary>
    public static TDragonFlyBuilder AddCore<TDragonFlyBuilder>(this TDragonFlyBuilder builder)
        where TDragonFlyBuilder : IDragonFlyBuilder
    {
        builder.Services.TryAddSingleton(FieldManager.Default);
        builder.Services.TryAddSingleton(BlockManager.Default);
        builder.Services.TryAddSingleton(MetadataManager.Default);
        builder.Services.TryAddSingleton(PermissionManager.Default);

        builder.Services.TryAddSingleton<IDragonFlyApi, DragonFlyApi>();
        builder.Services.TryAddSingleton<ISlugService, SlugService>();

        builder.Init(api =>
        {
            api.Field().AddDefaults();
            api.Block().AddDefaults();
            api.Metadata().AddDefaults();
            api.Permission().AddDefaults();
        });

        return builder;
    }

    /// <summary>
    /// Adds DragonFly core pipeline.
    /// </summary>
    public static IDragonFlyBuilder AddDragonFlyCore(this IServiceCollection services)
    {
        return new DragonFlyBuilder(services)
                                            .AddCore();
    }
}
