// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Client;

namespace DragonFly;

public static class DragonFlyApiExtensions
{
    public static void RegisterField<TField, TFieldView>(this IDragonFlyApi api)
        where TField : ContentField
        where TFieldView : IFieldComponent
    {
        api.ContentField().Add<TField>();
        api.Components().RegisterField<TFieldView>();
    }

    public static void RegisterField<TField, TFieldView, TFieldOptionsView>(this IDragonFlyApi api)
        where TField : ContentField, new()
        where TFieldView : IFieldComponent
        where TFieldOptionsView : IFieldOptionsComponent
    {
        api.RegisterField<TField, TFieldView>();
        api.Components().RegisterOptions<TFieldOptionsView>();
    }

    public static void RegisterField<TField, TFieldView, TFieldOptionsView, TFieldQueryView>(this IDragonFlyApi api)
        where TField : ContentField, new()
        where TFieldView : IFieldComponent
        where TFieldOptionsView : IFieldOptionsComponent
        where TFieldQueryView : IFieldQueryComponent
    {
        api.RegisterField<TField, TFieldView, TFieldOptionsView>();
        api.Components().RegisterQuery<TFieldQueryView>();
    }

    public static void RegisterMetadata<TMetadata, TMetadataView>(this IDragonFlyApi api)
        where TMetadata : AssetMetadata, new()
        where TMetadataView : IAssetMetadataComponent
    {
        api.AssetMetadata().Add<TMetadata>();
        api.Components().RegisterAssetMetadata<TMetadataView>();
    }
}
