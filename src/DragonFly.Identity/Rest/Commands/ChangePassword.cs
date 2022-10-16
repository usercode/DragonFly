// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

namespace DragonFly.Identity.Commands;

public class ChangePassword
{
    public Guid UserId { get; set; }

    public string NewPassword { get; set; }
}
