﻿using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace DragonFly.Contents.Assets
{
    public class Asset : ContentBase
    {
        public Asset()
        {
            _metaddata = new Dictionary<string, AssetMetadata>();
        }

        public Asset(Guid id)
           : this()
        {
            Id = id;
        }

        /// <summary>
        /// Name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Filename
        /// </summary>
        public virtual string Slug { get; set; }

        /// <summary>
        /// ContentType
        /// </summary>
        public virtual string MimeType { get; set; }

        /// <summary>
        /// Hash
        /// </summary>
        public virtual string Hash { get; set; }

        /// <summary>
        /// Size
        /// </summary>
        public virtual long Size { get; set; }

        /// <summary>
        /// Folder
        /// </summary>
        public virtual AssetFolder Folder { get; set; }

        private IDictionary<string, AssetMetadata> _metaddata;

        /// <summary>
        /// Metaddata
        /// </summary>
        public virtual IDictionary<string, AssetMetadata> Metaddata { get => _metaddata; set => _metaddata = value; }
    }
}
