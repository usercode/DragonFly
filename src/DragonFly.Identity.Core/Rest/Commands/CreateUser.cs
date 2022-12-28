// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity.Rest.Commands;

public class CreateUser
{
    public IdentityUser? User { get; set; }  

    public string? Password { get; set; }
}
