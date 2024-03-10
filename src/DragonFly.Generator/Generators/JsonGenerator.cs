//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis.CSharp.Syntax;

//namespace DragonFly.Generator;

//[Generator]
//public class JsonGenerator : IIncrementalGenerator
//{
//    public void Initialize(IncrementalGeneratorInitializationContext context)
//    {
//        var classProvider = context.SyntaxProvider
//                                   .CreateSyntaxProvider((node, _) =>
//                                   {
//                                       return node is ClassDeclarationSyntax syntax && syntax.AttributeLists.Count > 0;
//                                   },
//                                   (ctx, _) =>
//                                   {
//                                       return new { GeneratorContext = ctx, ClassSyntax = (ClassDeclarationSyntax)ctx.Node };
//                                   });

//        var r = classProvider.Collect();
        
//        context.RegisterSourceOutput(r, (ctx, ar) =>
//        {
//            List<INamedTypeSymbol> fields = new List<INamedTypeSymbol>();

//            foreach (var item in ar)
//            {
//                if (item.ClassSyntax.AttributeLists.SelectMany(x => x.Attributes).Any(x => x.Name.ToString() == "Field") == false)
//                {
//                    return;
//                }

//                INamedTypeSymbol? classSymbol = item.GeneratorContext.SemanticModel.GetDeclaredSymbol(item.ClassSyntax);

//                if (classSymbol == null)
//                {
//                    return;
//                }

//                fields.Add(classSymbol);
//            }

//            if (fields.Any())
//            {

//                SourceBuilder builder = new SourceBuilder();
//                builder.AppendNullableDirective();
//                builder.AddUsings("System.Text.Json.Serialization");
//                builder.AddNamespace("DragonFly", x =>
//                {
//                    //x.AppendLine("[JsonSerializable(typeof(ResourceCreated))]");
//                    //x.AppendLine("[JsonSerializable(typeof(RestContentItem))]");
//                    //x.AppendLine("[JsonSerializable(typeof(RestContentSchema))]");
//                    //x.AppendLine("[JsonSerializable(typeof(RestAsset))]");
//                    //x.AppendLine("[JsonSerializable(typeof(RestAssetFolder))]");
//                    //x.AppendLine("[JsonSerializable(typeof(QueryResult<RestAsset>))]");
//                    //x.AppendLine("[JsonSerializable(typeof(QueryResult<RestAssetFolder>))]");
//                    //x.AppendLine("[JsonSerializable(typeof(QueryResult<RestContentItem>))]");
//                    //x.AppendLine("[JsonSerializable(typeof(QueryResult<RestContentSchema>))]");
//                    //x.AppendLine("[JsonSerializable(typeof(QueryResult<RestContentStructure>))]");
//                    //x.AppendLine("[JsonSerializable(typeof(QueryResult<RestContentNode>))]");
//                    //x.AppendLine("[JsonSerializable(typeof(QueryResult<RestWebHook>))]");
//                    //x.AppendLine("[JsonSerializable(typeof(AssetQuery))]");
//                    //x.AppendLine("[JsonSerializable(typeof(AssetFolderQuery))]");
//                    //x.AppendLine("[JsonSerializable(typeof(ContentQuery))]");
//                    //x.AppendLine("[JsonSerializable(typeof(StructureQuery))]");
//                    x.AppendLine("[JsonSerializable(typeof(IBackgroundTaskInfo))]");
//                    x.AppendLine("[JsonSerializable(typeof(IEnumerable<IBackgroundTaskInfo>))]");

//                    //FieldOptions
//                    foreach (var f in fields)
//                    {
//                        x.AppendLine($"[JsonSerializable(typeof({f.ToDisplayString()})]");
//                    }

//                    x.AppendLine("public partial class ApiJsonSerializerContext2 : JsonSerializerContext");
//                    x.AppendBlock(x =>
//                    {

//                    });
//                });                   

//                ctx.AddSource($"DragonFly.JsonFieldInitializer.g.cs", builder.ToString());
//            }
//        });
//    }
//}
