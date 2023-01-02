// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Proxy;

public class ContentProxyBuilderOptions
{
    public ContentProxyBuilderOptions()
    {
        Types = new List<Type>();
    }

    public IList<Type> Types { get; }

    public ContentProxyBuilderOptions AddType<T>()
        where T : class, IContentModel, new()
    {
        Types.Add(typeof(T));

        return this;
    }
}
