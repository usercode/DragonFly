// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DragonFly.Permissions;

namespace DragonFly.Identity.Services;

internal class PermissionService : IPermissionService
{
    public PermissionService(IDragonFlyApi api)
    {
        Api = api;
    }

    public IDragonFlyApi Api { get; }

    public Task<IEnumerable<Permission>> GetPermissionsAsync()
    {
        return Task.FromResult(Api.Permission().GetAll());
    }
}
