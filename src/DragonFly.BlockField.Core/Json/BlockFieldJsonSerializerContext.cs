// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;

namespace DragonFly.BlockField;

[JsonSerializable(typeof(BlockField))]
[JsonSerializable(typeof(BlockFieldOptions))]
public partial class BlockFieldJsonSerializerContext : JsonSerializerContext
{

}
