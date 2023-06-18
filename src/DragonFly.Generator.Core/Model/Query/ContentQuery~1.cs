// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Query;

public class ContentQuery<TContentModel> : ContentQuery
    where TContentModel : class, IContentModel
{
    public ContentQuery()
    {
        Schema = TContentModel.Metadata.ModelName;
    }
}
