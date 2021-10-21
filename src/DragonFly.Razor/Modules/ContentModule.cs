using DragonFly.Assets;
using DragonFly.Content;
using DragonFly.Razor.Pages.ContentItems.Fields;
using DragonFly.Razor.Pages.ContentItems.Query;
using DragonFly.Razor.Pages.ContentSchemas.Fields;
using DragonFly.Razor.Services;
using DragonFly.Razor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonFly.Razor.Modules
{
    /// <summary>
    /// ContentModule
    /// </summary>
    public class ContentModule : ClientModule
    {
        public override string Name => "Content";

        public override string Description => "Manage content schema and items";

        public override string Author => "DragonFly";

        public override void Init(IDragonFlyApi api)
        {
            api.MainMenu().AddMenuItem("Schema", "oi oi-list-rich icon", "schema");
            api.MainMenu().AddMenuItem("Structure", "oi oi-list-rich icon", "structure");
            api.MainMenu().AddMenuItem("Content", "oi oi-list-rich icon", "content");

            api.RegisterField<ArrayField, ArrayFieldView, ArrayFieldOptionsView>();
            api.RegisterField<AssetField, AssetFieldView, AssetFieldOptionsView, AssetFieldQueryView>();
            api.RegisterField<ReferenceField, ReferenceFieldView, ReferenceFieldOptionsView, ReferenceFieldQueryView>();
            api.RegisterField<BoolField, BoolFieldView, BoolFieldOptionsView>();
            api.RegisterField<DateField, DateTimeFieldView>();
            api.RegisterField<StringField, StringFieldView, StringFieldOptionsView, StringFieldQueryView>();
            api.RegisterField<FloatField, FloatFieldView, FloatFieldOptionsView>();
            api.RegisterField<IntegerField, IntegerFieldView, IntegerFieldOptionsView, IntegerFieldQueryView>();
            api.RegisterField<SlugField, SlugFieldView>();
            api.RegisterField<TextAreaField, TextAreaFieldView>();
            api.RegisterField<HtmlField, HtmlFieldView>();
            api.RegisterField<XHtmlField, XHtmlFieldView>();
            api.RegisterField<XmlField, XmlFieldView>();
        }
    }
}
