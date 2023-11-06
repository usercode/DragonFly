// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly.BlockField;

[JsonSerializable(typeof(BlockField))]
[JsonSerializable(typeof(BlockFieldOptions))]

[JsonSerializable(typeof(Document))]

//Blocks
[JsonSerializable(typeof(AlertBlock))]
[JsonSerializable(typeof(AssetBlock))]
[JsonSerializable(typeof(CardsBlock))]
[JsonSerializable(typeof(ContainerBlock))]
[JsonSerializable(typeof(HeadingBlock))]
[JsonSerializable(typeof(HtmlBlock))]
[JsonSerializable(typeof(OpenGraphBlock))]
[JsonSerializable(typeof(ProgressBlock))]
[JsonSerializable(typeof(QuoteBlock))]
[JsonSerializable(typeof(ReferenceBlock))]
[JsonSerializable(typeof(SectionBlock))]
[JsonSerializable(typeof(SlideshowBlock))]
[JsonSerializable(typeof(TextBlock))]
[JsonSerializable(typeof(UnknownBlock))]
[JsonSerializable(typeof(YouTubeBlock))]

//defaults
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
public partial class BlockFieldJsonSerializerContext : JsonSerializerContext
{

}
