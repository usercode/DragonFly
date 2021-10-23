using DragonFly.Fields.BlockField.Blocks;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField.Storage.Json
{
    /// <summary>
    /// JsonDiscriminatorBinder
    /// </summary>
    public class JsonDiscriminatorBinder : DefaultSerializationBinder
    {
        public override void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
        {
            assemblyName = null;

            if (BlockFieldManager.Default.TryGetBlockNameByType(serializedType, out typeName) == false)
            {
                throw new Exception($"Block '{typeName}' was not found.");
            }
        }

        public override Type BindToType(string? assemblyName, string typeName)
        {
            if (BlockFieldManager.Default.TryGetBlockTypeByName(typeName, out Type? type))
            {
                return type;
            }

            return typeof(UnknownBlock);
        }
    }
}
