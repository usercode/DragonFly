// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json.Serialization;
using DragonFly.Query;

namespace DragonFly.API;

[JsonSerializable(typeof(ResourceCreated))]
[JsonSerializable(typeof(RestContentItem))]
[JsonSerializable(typeof(RestContentSchema))]
[JsonSerializable(typeof(RestAsset))]
[JsonSerializable(typeof(RestAssetFolder))]
[JsonSerializable(typeof(QueryResult<RestAsset>))]
[JsonSerializable(typeof(QueryResult<RestAssetFolder>))]
[JsonSerializable(typeof(QueryResult<RestContentItem>))]
[JsonSerializable(typeof(QueryResult<RestContentSchema>))]
[JsonSerializable(typeof(QueryResult<RestContentStructure>))]
[JsonSerializable(typeof(QueryResult<RestContentNode>))]
[JsonSerializable(typeof(QueryResult<RestWebHook>))]
[JsonSerializable(typeof(AssetQuery))]
[JsonSerializable(typeof(AssetFolderQuery))]
[JsonSerializable(typeof(ContentQuery))]
[JsonSerializable(typeof(StructureQuery))]
[JsonSerializable(typeof(IBackgroundTaskInfo))]
[JsonSerializable(typeof(IEnumerable<IBackgroundTaskInfo>))]
[JsonSerializable(typeof(WebHookQuery))]

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
[JsonSerializable(typeof(XHtmlFieldOptions))]
[JsonSerializable(typeof(XmlFieldOptions))]

[JsonSourceGenerationOptions(PropertyNameCaseInsensitive = true)]
public partial class ApiJsonSerializerContext : JsonSerializerContext
{
}
