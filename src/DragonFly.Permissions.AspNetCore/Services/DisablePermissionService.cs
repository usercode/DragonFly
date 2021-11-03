using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions.AspNetCore.Services
{
    class DisablePermissionService : IPermissionService
    {
        public DisablePermissionService(IPermissionService permissionService)
        {
            Service = permissionService;
        }

        private IPermissionService Service { get; }

        public async Task AuthorizeAsync(string permission)
        {
            if (DisablePermissionState.Disabled.Value)
            {
                return;
            }

            await Service.AuthorizeAsync(permission);
        }
    }
}
