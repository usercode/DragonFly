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
    /// ElementConverter
    /// </summary>
    public class JsonDiscriminatorBinder : DefaultSerializationBinder
    {
        public override void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
        {
            assemblyName = null;
            typeName = BlockFieldManager.Default.GetElementNameByType(serializedType);
        }

        public override Type BindToType(string? assemblyName, string typeName)
        {
            return BlockFieldManager.Default.GetElementTypeByName(typeName);
        }
    }
}
