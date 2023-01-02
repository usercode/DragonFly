// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy;

namespace DragonFly.Query;

public class ContentQuery<TContentModel> : ContentQuery
    where TContentModel : class, IContentModel
{
    public ContentQuery()
    {
        Schema = ProxyTypeManager.Default.GetSchema<TContentModel>().Name;
    }
}
