// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Identity.Commands;

public class ChangePassword
{
    public Guid UserId { get; set; }

    public string NewPassword { get; set; }
}
