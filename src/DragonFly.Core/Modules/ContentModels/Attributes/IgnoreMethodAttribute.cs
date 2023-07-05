// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Generator;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class IgnoreMethodAttribute : Attribute
{
    public IgnoreMethodAttribute(string method)
    {
        Method = method;
    }

    public string Method { get; }
}
