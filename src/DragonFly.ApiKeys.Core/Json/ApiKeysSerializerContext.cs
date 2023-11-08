// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly.ApiKeys;

//Models
[JsonSerializable(typeof(ApiKey))]
[JsonSerializable(typeof(IEnumerable<ApiKey>))]

//Defaults
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
public partial class ApiKeysSerializerContext : JsonSerializerContext
{
}
