// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization;
using DragonFly.AspNetCore.Exports;
using DragonFly.Identity.Commands;
using DragonFly.Identity.Rest.Commands;

namespace DragonFly.Identity;

//Models
[JsonSerializable(typeof(IdentityUser))]
[JsonSerializable(typeof(IdentityRole))]
[JsonSerializable(typeof(IEnumerable<IdentityUser>))]
[JsonSerializable(typeof(IEnumerable<IdentityRole>))]
[JsonSerializable(typeof(IEnumerable<Permission>))]

//Actions
[JsonSerializable(typeof(LoginData))]
[JsonSerializable(typeof(ChangePassword))]
[JsonSerializable(typeof(CreateUser))]

//Defaults
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
public partial class IdentitySerializerContext : JsonSerializerContext
{
}
