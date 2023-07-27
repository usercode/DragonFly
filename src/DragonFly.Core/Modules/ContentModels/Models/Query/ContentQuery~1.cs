// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

/// <summary>
/// Content query for <typeparamref name="TContentModel"/>.
/// </summary>
public class ContentQuery<TContentModel> : ContentQuery
    where TContentModel : class, IContentModel
{
    public ContentQuery()
    {
        Schema = TContentModel.Schema.Name;
    }
}
