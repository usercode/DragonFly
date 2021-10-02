using DragonFly.Fields.BlockField.Blocks;
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

                    _default.RegisterBlock<ColumnBlock>();
                    _default.RegisterBlock<ImageBlock>();
                    _default.RegisterBlock<TextBlock>();
                    _default.RegisterBlock<HtmlBlock>();
                }

                return _default;
            }
        }

        private IDictionary<string, Type> _elementsByName;
        private IDictionary<Type, string> _elementsByType;

        private BlockFieldManager()
        {
            _elementsByName = new Dictionary<string, Type>();
            _elementsByType = new Dictionary<Type, string>();
        }

        public IEnumerable<Type> GetAllBlockTypes()
        {
            return _elementsByType.Keys.ToList();
        }

        public IEnumerable<Block> GetAllBlocks()
        {
            return _elementsByType.Keys.Select(x => (Block?)Activator.CreateInstance(x)).ToList();
        }

        /// <summary>
        /// RegisterElement
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        public void RegisterBlock<TBlock>()
            where TBlock : Block
        {
            _elementsByName.Add(typeof(TBlock).Name, typeof(TBlock));
            _elementsByType.Add(typeof(TBlock), typeof(TBlock).Name);
        }

        public Type GetBlockTypeByName(string name)
        {
            if (_elementsByName.TryGetValue(name, out Type? type))
            {
                return type;
            }

            throw new Exception($"Block '{name}' not found.");
        }

        public string GetBlockNameByType(Type type)
        {
            if (_elementsByType.TryGetValue(type, out string? name))
            {
                return name;
            }

            throw new Exception($"Block '{type.Name}' not found.");
        }
    }
}
