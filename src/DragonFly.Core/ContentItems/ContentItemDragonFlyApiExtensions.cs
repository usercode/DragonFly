﻿using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    /// <summary>
    /// ContentItemDragonFlyApiExtensions
    /// </summary>
    public static class ContentItemDragonFlyApiExtensions
    {
        public static ContentFieldManager ContentField(this IDragonFlyApi dragonFlyApi)
        {
            return ContentFieldManager.Default;
        }

        public static ContentFieldManager AddDefaults(this ContentFieldManager manager)
        {
            manager.Add<ArrayField>();
            manager.Add<AssetField>();
            manager.Add<BoolField>();
            manager.Add<DateField>();
            manager.Add<EmbedField>();
            manager.Add<FloatField>();
            manager.Add<HtmlField>();
            manager.Add<IntegerField>();
            manager.Add<ReferenceField>();
            manager.Add<SlugField>();
            manager.Add<StringField>();
            manager.Add<TextAreaField>();
            manager.Add<XHtmlField>();
            manager.Add<XmlField>();

            return manager;
        }
    }
}