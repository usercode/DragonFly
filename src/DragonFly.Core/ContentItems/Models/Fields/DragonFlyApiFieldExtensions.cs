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
        public static IDragonFlyApi RegisterField<TContentField>(this IDragonFlyApi api)
            where TContentField : ContentField, new()
        {
            api.ContentField().RegisterField<TContentField>();

            return api;
        }

        public static IDragonFlyApi RegisterDefaultFields(this IDragonFlyApi api)
        {
            api.RegisterField<ArrayField>();
            api.RegisterField<AssetField>();
            api.RegisterField<BoolField>();
            api.RegisterField<DateField>();
            api.RegisterField<EmbedField>();
            api.RegisterField<FloatField>();
            api.RegisterField<HtmlField>();
            api.RegisterField<IntegerField>();
            api.RegisterField<ReferenceField>();
            api.RegisterField<SlugField>();
            api.RegisterField<StringField>();
            api.RegisterField<TextAreaField>();
            api.RegisterField<XHtmlField>();
            api.RegisterField<XmlField>();

            return api;
        }
    }
}
