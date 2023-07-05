// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// ContentQuery
/// </summary>
/// <typeparam name="TContentModel"></typeparam>
public class ContentQuery<TContentModel> : ContentQuery
    where TContentModel : class, IContentModel
{
    public ContentQuery()
    {
        Schema = TContentModel.Schema.Name;
    }
}
