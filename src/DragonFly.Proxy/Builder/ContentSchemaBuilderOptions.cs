// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Proxy;

public class ContentSchemaBuilderOptions
{
    public ContentSchemaBuilderOptions()
    {
        Types = new List<Type>();
    }

    public IList<Type> Types { get; }

    public ContentSchemaBuilderOptions AddType<T>()
        where T : class
    {
        Types.Add(typeof(T));

        return this;
    }
}
