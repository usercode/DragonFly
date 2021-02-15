using DragonFly.Data;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.AspNetCore
{
    public class TypeNameSerializationBinder : DefaultSerializationBinder
    {
        public TypeNameSerializationBinder(FieldTypeDiscriminatorMapper discriminator)
        {
            Discriminator = discriminator;
        }

        private FieldTypeDiscriminatorMapper Discriminator { get; }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = Discriminator.GetDiscriminator(serializedType);
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            return Discriminator.GetType(typeName);
        }

    }
}
