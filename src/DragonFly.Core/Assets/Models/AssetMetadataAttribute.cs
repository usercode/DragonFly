// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

[AttributeUsage(AttributeTargets.Class)]
public class AssetMetadataAttribute : Attribute
{
    public AssetMetadataAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}
