// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;
using DragonFly.AspNetCore.Exports;
using DragonFly.Identity.Commands;
using DragonFly.Identity.Rest.Commands;

namespace DragonFly.Identity;

[JsonSerializable(typeof(LoginData))]
[JsonSerializable(typeof(CreateUser))]
[JsonSerializable(typeof(ChangePassword))]
public partial class ApiJsonSerializerContext : JsonSerializerContext
{
}
