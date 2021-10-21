using DragonFly.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly
{
    public static class DragonFlyApiFieldExtensions
    {
        public static IDragonFlyApi RegisterDefaultFields(this IDragonFlyApi api)
        {
            api.ContentField().Register<ArrayField>();
            api.ContentField().Register<AssetField>();
            api.ContentField().Register<BoolField>();
            api.ContentField().Register<DateField>();
            api.ContentField().Register<EmbedField>();
            api.ContentField().Register<FloatField>();
            api.ContentField().Register<HtmlField>();
            api.ContentField().Register<IntegerField>();
            api.ContentField().Register<ReferenceField>();
            api.ContentField().Register<SlugField>();
            api.ContentField().Register<StringField>();
            api.ContentField().Register<TextAreaField>();
            api.ContentField().Register<XHtmlField>();
            api.ContentField().Register<XmlField>();

            return api;
        }
    }
}
