// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization;
using DragonFly.Query;

namespace DragonFly.API;

//Model
[JsonSerializable(typeof(ResourceCreated))]
[JsonSerializable(typeof(RestContentItem))]
[JsonSerializable(typeof(RestContentSchema))]
[JsonSerializable(typeof(RestAsset))]
[JsonSerializable(typeof(RestAssetFolder))]
[JsonSerializable(typeof(QueryResult<RestAsset>))]
[JsonSerializable(typeof(QueryResult<RestAssetFolder>))]
[JsonSerializable(typeof(QueryResult<RestContentItem>))]
[JsonSerializable(typeof(QueryResult<ContentVersionEntry>))]
[JsonSerializable(typeof(QueryResult<RestContentSchema>))]
[JsonSerializable(typeof(QueryResult<RestContentStructure>))]
[JsonSerializable(typeof(QueryResult<RestContentNode>))]
[JsonSerializable(typeof(QueryResult<RestWebHook>))]
[JsonSerializable(typeof(AssetQuery))]
[JsonSerializable(typeof(AssetFolderQuery))]
[JsonSerializable(typeof(ContentQuery))]
[JsonSerializable(typeof(StructureQuery))]
[JsonSerializable(typeof(WebHookQuery))]
[JsonSerializable(typeof(IBackgroundTaskInfo))]
[JsonSerializable(typeof(BackgroundTaskInfo))]
[JsonSerializable(typeof(IEnumerable<IBackgroundTaskInfo>))]
[JsonSerializable(typeof(ContentVersionEntry))]
[JsonSerializable(typeof(IEnumerable<ContentVersionEntry>))]

//AssetMetadata
[JsonSerializable(typeof(ImageMetadata))]
[JsonSerializable(typeof(PdfMetadata))]
[JsonSerializable(typeof(VideoMetadata))]

//Field
[JsonSerializable(typeof(ArrayField))]
[JsonSerializable(typeof(AssetField))]
[JsonSerializable(typeof(BoolField))]
[JsonSerializable(typeof(ColorField))]
[JsonSerializable(typeof(ComponentField))]
[JsonSerializable(typeof(DateTimeField))]
[JsonSerializable(typeof(FloatField))]
[JsonSerializable(typeof(GeolocationField))]
[JsonSerializable(typeof(HtmlField))]
[JsonSerializable(typeof(IntegerField))]
[JsonSerializable(typeof(ReferenceField))]
[JsonSerializable(typeof(SlugField))]
[JsonSerializable(typeof(StringField))]
[JsonSerializable(typeof(TextField))]
[JsonSerializable(typeof(XmlField))]

//FieldOptions
[JsonSerializable(typeof(ArrayFieldOptions))]
[JsonSerializable(typeof(AssetFieldOptions))]
[JsonSerializable(typeof(BoolFieldOptions))]
[JsonSerializable(typeof(ColorFieldOptions))]
[JsonSerializable(typeof(ComponentFieldOptions))]
[JsonSerializable(typeof(DateTimeFieldOptions))]
[JsonSerializable(typeof(FloatFieldOptions))]
[JsonSerializable(typeof(GeolocationFieldOptions))]
[JsonSerializable(typeof(HtmlFieldOptions))]
[JsonSerializable(typeof(IntegerFieldOptions))]
[JsonSerializable(typeof(ReferenceFieldOptions))]
[JsonSerializable(typeof(SlugFieldOptions))]
[JsonSerializable(typeof(StringFieldOptions))]
[JsonSerializable(typeof(TextFieldOptions))]
[JsonSerializable(typeof(XmlFieldOptions))]

//FieldQuery
[JsonSerializable(typeof(StringFieldQuery))]
[JsonSerializable(typeof(BoolFieldQuery))]
[JsonSerializable(typeof(FloatFieldQuery))]
[JsonSerializable(typeof(GeolocationFieldQuery))]
[JsonSerializable(typeof(IntegerFieldQuery))]
[JsonSerializable(typeof(ReferenceFieldQuery))]
[JsonSerializable(typeof(SlugFieldQuery))]
[JsonSerializable(typeof(StringFieldQuery))]

//defaults
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
internal partial class ApiJsonSerializerContext : JsonSerializerContext
{
}
