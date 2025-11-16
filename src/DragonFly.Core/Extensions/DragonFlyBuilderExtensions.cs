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
    /// <see cref="BoolField"/>, <see cref="IntegerField"/>, <see cref="FloatField"/>, <see cref="ColorField"/>, <see cref="GeolocationField"/>, <see cref="BlockField"/>,<br />
    /// <see cref="StringField"/>, <see cref="TextField"/>, <see cref="SlugField"/>, <see cref="HtmlField"/>,<br />
    /// <see cref="DateField"/>, <see cref="TimeField"/>, <see cref="DateTimeField"/>,<br />
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
            api.Fields.AddDefaults();
            api.Blocks.AddDefaults();
            api.Metadatas.AddDefaults();
            api.Permissions.AddDefaults();
        });

        return builder;
    }

    /// <summary>
    /// Adds DragonFly core pipeline.
    /// </summary>
    public static IDragonFlyBuilder AddDragonFlyCore(this IServiceCollection services)
    {
        return new DragonFlyBuilder(services).AddCore();
    }
}
