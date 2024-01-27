// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using DragonFly.Client;

namespace DragonFly;

public static class DragonFlyApiExtensions
{
    //public static IFieldAdded<T> WithFieldView2<T>(this IFieldAdded<T> field, Func<IFieldComponent<T>> component)
    //    where T : ContentField
    //{
    //    ComponentManager.Default.Add(typeof(T), typeof(TFieldView));

    //    return field;
    //}

    public static IFieldAdded WithFieldView<TFieldView>(this IFieldAdded field)
        where TFieldView : IFieldComponent, new()
    {
        Type fieldType = new TFieldView().FieldType;

        ComponentManager.Default.Add(fieldType, typeof(TFieldView));

        return field;
    }

    public static IFieldAdded WithOptionView<TFieldOptionsView>(this IFieldAdded field)       
        where TFieldOptionsView : IFieldOptionsComponent, new()
    {
        Type optionsType = new TFieldOptionsView().OptionsType;

        ComponentManager.Default.Add(optionsType, typeof(TFieldOptionsView));

        return field;
    }

    public static IFieldAdded WithQueryView<TFieldQueryView>(this IFieldAdded field)
        where TFieldQueryView : IFieldQueryComponent, new()
    {
        Type queryType = new TFieldQueryView().QueryType;

        ComponentManager.Default.Add(queryType, typeof(TFieldQueryView));

        return field;
    }

    public static AssetMetadataAdded WithMetadataView<TMetadataView>(this AssetMetadataAdded field)
        where TMetadataView : IAssetMetadataComponent, new()
    {
        Type metadataType = new TMetadataView().MetadataType;

        ComponentManager.Default.Add(metadataType, typeof(TMetadataView));

        return field;
    }
}
