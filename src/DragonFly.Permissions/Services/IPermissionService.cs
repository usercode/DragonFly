using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Permissions;

public interface IPermissionService
{
    Task<IEnumerable<Permission>> GetPermissionsAsync();
}
