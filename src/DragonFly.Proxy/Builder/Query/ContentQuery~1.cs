// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Net.Mime;
using DragonFly.Query;

namespace DragonFly.Proxy.Query;

public class ContentQuery<TContentModel> : ContentQuery, IContentQuery<TContentModel>
    where TContentModel : class, IContentModel
{
    public ContentQuery()
    {
        Schema = ProxyTypeManager.Default.GetSchema<TContentModel>().Name;
    }
}
