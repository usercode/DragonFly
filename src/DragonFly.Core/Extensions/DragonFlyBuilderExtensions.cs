// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization.Metadata;
using DragonFly.Builders;
using DragonFly.Init;
using Microsoft.Extensions.DependencyInjection;

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
        builder.Services.AddSingleton(FieldManager.Default);
        builder.Services.AddSingleton(BlockManager.Default);
        builder.Services.AddSingleton(MetadataManager.Default);

        builder.Init(api =>
        {
            api.Field().AddDefaults();
            api.Block().AddDefaults();
            api.Metadata().AddDefaults();
        });

        builder.Services.AddSingleton<IDragonFlyApi, DragonFlyApi>();
        builder.Services.AddSingleton<ISlugService, SlugService>();

        return builder;
    }

    /// <summary>
    /// Adds DragonFly core pipeline.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IDragonFlyBuilder AddDragonFlyCore(this IServiceCollection services)
    {
        return new DragonFlyBuilder(services)
                                            .AddCore();
    }

    public static TDragonFlyBuilder AddBlockFieldSerializerResolver<TDragonFlyBuilder>(this TDragonFlyBuilder builder, IJsonTypeInfoResolver resolver)
       where TDragonFlyBuilder : IDragonFlyBuilder
    {
        BlockFieldSerializer.Options.TypeInfoResolverChain.Add(resolver);

        return builder;
    }
}
