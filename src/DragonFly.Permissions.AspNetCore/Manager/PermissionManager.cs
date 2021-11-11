using DragonFly.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Core.Permissions
{
    public class TraverseNode
    {
        public TraverseNode(Permission permission, IList<Permission> path)
        {
            Permission = permission;
            Path = path;
        }

        public IList<Permission> Path;

        public Permission Permission;
    }

    /// <summary>
    /// PermissionManager
    /// </summary>
    public class PermissionManager : IPermission
    {
        private static PermissionManager? _default;

        public static PermissionManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new PermissionManager();
                }

                return _default;
            }
        }

        public PermissionManager()
        {
            Items = new List<Permission>();
            Policies = new Dictionary<string, IList<string>>();
        }

        public event Action<Permission> Added;

        /// <summary>
        /// Items
        /// </summary>
        public IList<Permission> Items { get; }

        public Permission Add(Permission permissionItem)
        {
            if (_policyBuilt)
            {
                throw new InvalidOperationException("The PermissionManager is already fixed.");
            }

            Permission? found = Items.FirstOrDefault(x => x.Name == permissionItem.Name);

            if (found != null)
            {
                return found;
            }

            Items.Add(permissionItem);

            Added?.Invoke(permissionItem);

            return permissionItem;
        }

        private IDictionary<string, IList<string>> Policies { get; }
        private bool _policyBuilt;

        public IEnumerable<string> GetPolicy(string name)
        {
            if (_policyBuilt == false)
            {
                BuildPolicies();

                _policyBuilt = true;
            }

            if (Policies.TryGetValue(name, out IList<string>? policies))
            {
                return policies;
            }

            return Array.Empty<string>();
        }

        private void BuildPolicies()
        {
            Policies.Clear();

            Items.Traverse(x =>
            {
                Policies.Add(x.Permission.Name, x.Path.Select(p => p.Name).ToList());
            });
        }        
    }
}
