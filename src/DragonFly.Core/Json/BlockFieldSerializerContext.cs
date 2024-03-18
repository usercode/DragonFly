// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using System.Text.Json;
using System.Text.Json.Serialization;

namespace DragonFly;

[JsonSerializable(typeof(Document))]

//Blocks
[JsonSerializable(typeof(IEnumerable<Block>))]

[JsonSerializable(typeof(AlertBlock))]
[JsonSerializable(typeof(AssetBlock))]
[JsonSerializable(typeof(CardsBlock))]
[JsonSerializable(typeof(CodeBlock))]
[JsonSerializable(typeof(ContainerBlock))]
[JsonSerializable(typeof(ColumnBlock))]
[JsonSerializable(typeof(GridBlock))]
[JsonSerializable(typeof(HeadingBlock))]
[JsonSerializable(typeof(HtmlBlock))]
[JsonSerializable(typeof(OpenGraphBlock))]
[JsonSerializable(typeof(ProgressBlock))]
[JsonSerializable(typeof(QuoteBlock))]
[JsonSerializable(typeof(ReferenceBlock))]
[JsonSerializable(typeof(SectionBlock))]
[JsonSerializable(typeof(SlideshowBlock))]
[JsonSerializable(typeof(TextBlock))]
[JsonSerializable(typeof(YouTubeBlock))]

//defaults
[JsonSourceGenerationOptions(JsonSerializerDefaults.Web)]
internal partial class BlockFieldSerializerContext : JsonSerializerContext
{

}
