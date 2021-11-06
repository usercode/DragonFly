﻿using DragonFly.ContentItems;
using DragonFly.Core.Permissions;
using DragonFly.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class PermissionDragonFlyApiExtensions
    {
        public static PermissionManager Permission(this IDragonFlyApi api)
        {
            return PermissionManager.Default;
        }

        public static IPermission AddDefaults(this PermissionManager manager)
        {
            manager
                    .Add(ContentPermissions.Content, x => x
                                .Add(ContentPermissions.ContentRead, description: "Read content", sortkey: 0, childs: x => x
                                            .Add(ContentPermissions.ContentQuery, description: "Query content"))
                                .Add(ContentPermissions.ContentCreate, description: "Create content", sortkey: 1)
                                .Add(ContentPermissions.ContentUpdate, description: "Update content", sortkey: 2)
                                .Add(ContentPermissions.ContentDelete, description: "Delete content", sortkey: 3)
                                )
                    .Add(SchemaPermissions.Schema, x => x
                                .Add(SchemaPermissions.SchemaRead, description: "Read schema", childs: x => x
                                            .Add(SchemaPermissions.SchemaQuery, description: "Query schema"))
                                .Add(SchemaPermissions.SchemaCreate, description: "Create schema")
                                .Add(SchemaPermissions.SchemaUpdate, description: "Update schema")
                                .Add(SchemaPermissions.SchemaDelete, description: "Delete schema")
                                )
                    .Add(AssetPermissions.Asset, x => x
                                .Add(AssetPermissions.AssetRead, description: "Read asset", sortkey: 0, childs: x => x
                                            .Add(AssetPermissions.AssetQuery, description: "Query asset"))
                                .Add(AssetPermissions.AssetCreate, description: "Create asset", sortkey: 1)
                                .Add(AssetPermissions.AssetUpdate, description: "Update asset", sortkey: 2)
                                .Add(AssetPermissions.AssetDelete, description: "Delete asset", sortkey: 3)
                                .Add(AssetPermissions.AssetUpload, description: "Upload asset", sortkey: 4)
                                .Add(AssetPermissions.AssetDownload, description: "Download asset", sortkey: 5)
                                );

            return manager;
        }

        public static IPermission Add(this IPermission manager, string name, Action<IPermission>? childs = null, string description = "", int sortkey = 0)
        {
            Permission p = new Permission(name);
            p.Description = description;
            p.SortKey = sortkey;

            p = manager.Add(p);

            childs?.Invoke(p);

            return manager;
        }       
    }
}