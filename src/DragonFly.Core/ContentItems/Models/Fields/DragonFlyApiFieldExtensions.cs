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
            api.ContentField().Add<ArrayField>();
            api.ContentField().Add<AssetField>();
            api.ContentField().Add<BoolField>();
            api.ContentField().Add<DateField>();
            api.ContentField().Add<EmbedField>();
            api.ContentField().Add<FloatField>();
            api.ContentField().Add<HtmlField>();
            api.ContentField().Add<IntegerField>();
            api.ContentField().Add<ReferenceField>();
            api.ContentField().Add<SlugField>();
            api.ContentField().Add<StringField>();
            api.ContentField().Add<TextAreaField>();
            api.ContentField().Add<XHtmlField>();
            api.ContentField().Add<XmlField>();

            return api;
        }
    }
}
