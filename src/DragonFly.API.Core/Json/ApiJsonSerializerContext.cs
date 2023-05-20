// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;

namespace DragonFly.API;

[JsonSerializable(typeof(ResourceCreated))]
[JsonSerializable(typeof(RestContentItem))]
[JsonSerializable(typeof(RestContentSchema))]
[JsonSerializable(typeof(RestAsset))]
[JsonSerializable(typeof(RestAssetFolder))]
public partial class ApiJsonSerializerContext : JsonSerializerContext
{
}
