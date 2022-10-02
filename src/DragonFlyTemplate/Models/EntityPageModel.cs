// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DragonFlyTemplate.Models;

public class EntityPageModel : BasePageModel
{
    public virtual Guid Id { get; }

}
