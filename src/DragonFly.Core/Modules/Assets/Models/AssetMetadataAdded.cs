// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly;

public sealed class AssetMetadataAdded
{
    public AssetMetadataAdded(Type fieldType)
    {
        FieldType = fieldType;
    }

    /// <summary>
    /// FieldType
    /// </summary>
    public Type FieldType { get; }
}
