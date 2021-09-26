using DragonFly.Content;
using DragonFly.Fields.BlockField.Storage.Serializers;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DragonFly.Fields.BlockField
{
    /// <summary>
    /// BlockField
    /// </summary>
    public class BlockField : SingleValueContentField<string>
    {
        public BlockField()
        {
            Document = new Document();
        }

        /// <summary>
        /// Document
        /// </summary>
        public Document? Document { get; set; }

        public override string? Value 
        {
            get => SerializeDocument();
            set => Document = Deserialize(value);
        }

        private string SerializeDocument()
        {
            return DocumentSerializer.Serialize(Document);
        }

        private Document? Deserialize(string? json)
        {
            if (json == null)
            {
                return new Document();
            }

            return DocumentSerializer.Deserialize(json);
        }
    }
}