using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragonFly.Data
{
    public class FieldTypeDiscriminatorMapper
    {
        private IDictionary<string, Type> _discriminatorToType = new Dictionary<string, Type>();
        private IDictionary<Type, string> _discriminatorToString = new Dictionary<Type, string>();

        public void AddType(Type type)
        {
            string discriminator = type.Name;

            _discriminatorToType.Add(discriminator, type);
            _discriminatorToString.Add(type, discriminator);
        }

        public string GetDiscriminator(Type type)
        {
            return _discriminatorToString[type];
        }

        public Type GetType(string discriminator)
        {
            return _discriminatorToType[discriminator];
        }
    }
}
