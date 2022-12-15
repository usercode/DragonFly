// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Mime;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

public class ContentQuery<TModel> : ContentQuery, IContentQuery<TModel>
{
    public ContentQuery()
    {
        Schema = ProxyTypeManager.Default.Get<TModel>().Name;
    }
}
