using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// BlockFieldManager
    /// </summary>
    public class BlockFieldManager
    {
        private static BlockFieldManager? _default;

        /// <summary>
        /// Default
        /// </summary>
        public static BlockFieldManager Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new BlockFieldManager();
                }

                return _default;
            }
        }

        private IDictionary<string, Type> _elementsByName;
        private IDictionary<Type, string> _elementsByType;

        public BlockFieldManager()
        {
            _elementsByName = new Dictionary<string, Type>();
            _elementsByType = new Dictionary<Type, string>();
        }

        public IEnumerable<Type> GetAllTypes()
        {
            return _elementsByType.Keys.ToList();
        }

        public IEnumerable<Element> GetAllElements()
        {
            return _elementsByType.Keys.Select(x => (Element?)Activator.CreateInstance(x)).ToList();
        }

        /// <summary>
        /// RegisterElement
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        public void RegisterElement<TElement>()
            where TElement : Element
        {
            _elementsByName.Add(typeof(TElement).Name, typeof(TElement));
            _elementsByType.Add(typeof(TElement), typeof(TElement).Name);
        }

        public Type GetElementTypeByName(string name)
        {
            if (_elementsByName.TryGetValue(name, out Type? type))
            {
                return type;
            }

            throw new Exception($"Element '{name}' not found.");
        }

        public string GetElementNameByType(Type type)
        {
            if (_elementsByType.TryGetValue(type, out string? name))
            {
                return name;
            }

            throw new Exception($"Element '{type.Name}' not found.");
        }
    }
}
