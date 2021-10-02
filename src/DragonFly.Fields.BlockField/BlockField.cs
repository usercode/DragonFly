using DragonFly.Content;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// BlockField
    /// </summary>
    [FieldOptions(typeof(BlockFieldOptions))]
    public class BlockField : SingleValueContentField<string>
    {
        public BlockField()
        {
        }
    }
}