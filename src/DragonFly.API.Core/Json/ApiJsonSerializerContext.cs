// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;
using DragonFly.Query;

namespace DragonFly.API;

[JsonSerializable(typeof(ResourceCreated))]
[JsonSerializable(typeof(RestContentItem))]
[JsonSerializable(typeof(RestContentSchema))]
[JsonSerializable(typeof(RestAsset))]
[JsonSerializable(typeof(RestAssetFolder))]
[JsonSerializable(typeof(QueryResult<RestAsset>))]
[JsonSerializable(typeof(QueryResult<RestContentItem>))]
[JsonSerializable(typeof(QueryResult<RestContentSchema>))]
[JsonSerializable(typeof(ContentQuery))]
public partial class ApiJsonSerializerContext : JsonSerializerContext
{
}
