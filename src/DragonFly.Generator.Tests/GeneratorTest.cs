//// Copyright (c) usercode
//// https://github.com/usercode/DragonFly
//// MIT License

//using Microsoft.CodeAnalysis.CSharp;
//using Microsoft.CodeAnalysis;
//using Xunit;
//using DragonFly.Generator;

//namespace DragonFly.Tests;

//public class GeneratorTest
//{
//    public GeneratorTest()
//    {
//        CustomerSchema = new ContentSchema("Customer");
//        CustomerSchema.AddString("Lastname");
//        CustomerSchema.AddString("Firstname");
//        CustomerSchema.AddBool("IsActive");
//        CustomerSchema.AddInteger("Value");
//        CustomerSchema.AddSlug("Slug");

        
//    }

//    private ContentSchema CustomerSchema { get; }

//    [Fact]
//    public void GeneratesEnumExtensionsCorrectly()
//    {
//        // The source code to test
//        var source = """
//                    namespace DragonFly.Generator.Tests;

//                    [ContentItem("TestContent")]
//                    public partial class Product
//                    {
//                        [StringField]
//                        public string? _title;

//                        [BoolField]
//                        public bool? _title;

//                        [SlugField]
//                        public SlugField _title;
//                    }
                    
//                    """;

//        // Pass the source code to our helper and snapshot test the output
//        Verify<ModelGenerator>(source);
//    }

//    [Fact]
//    public void TestFieldGenerator()
//    {
//        // The source code to test
//        var source = """
//                    namespace DragonFly;

//                    [ContentField]
//                    [FieldOptions(typeof(AssetOptions))]
//                    [FieldQuery(typeof(AssetQuery))]
//                    public partial class AssetField
//                    {
//                    }
                    
//                    """;

//        // Pass the source code to our helper and snapshot test the output
//        Verify<FieldGenerator>(source);
//    }

//    [Fact]
//    public void TestProxy()
//    {
//        // The source code to test
//        var source = """
//                    using DragonFly;
//                    using DragonFly.Generator;

//                    namespace DragonFly.Generator.Tests;

//                    public class Asset { }

//                    [Proxy]
//                    partial class AssetProxy : Asset
//                    {
//                            [Intercept]
//                            [IgnoreProperty(nameof(Asset))]
//                            public Task Do()
//                            {
//                            }
//                    }                    
//                    """;

//        // Pass the source code to our helper and snapshot test the output
//        Verify<ProxyGenerator>(source);
//    }

//    public static Task Verify<T>(string source)
//        where T : IIncrementalGenerator, new()
//    {
//        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
//        // Create references for assemblies we require
//        // We could add multiple references if required
//        IEnumerable<PortableExecutableReference> references = new[]
//        {
//            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
//        };

//        CSharpCompilation compilation = CSharpCompilation.Create(
//            assemblyName: "Tests",
//            syntaxTrees: new[] { syntaxTree },
//            references: references); // 👈 pass the references to the compilation

//        T generator = new T();

//        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

//        driver = driver.RunGenerators(compilation);

//        return Verifier
//            .Verify(driver)
//            .UseDirectory("Snapshots");
//    }

//    [Fact]
//    public void GetSuitableModelType()
//    {
//        ContentItem content = CustomerSchema.CreateContent();
//        content.SetValue("Lastname", "Doe");
//        content.SetValue("Firstname", "John");

//        Customer model = content.ToModel<Customer>();

//        Assert.True(model is Customer);
//    }

//    [Fact]
//    public void GetModelProperty()
//    {
//        ContentItem content = CustomerSchema.CreateContent();
//        content.Id = Guid.NewGuid();
//        content.SetValue("Lastname", "Doe");
//        content.SetValue("Firstname", "John");
//        content.SetValue<bool?>("IsActive", true);
//        content.SetValue<long?>("Value", 123);
//        content.SetValue("Slug", "my-path");

//        Customer customer = content.ToModel<Customer>();

//        Assert.Equal(content.Id, customer.Id);
//        Assert.Equal("Doe", customer.Lastname);
//        Assert.Equal("John", customer.Firstname);
//        Assert.Equal("my-path", customer.Slug.Value);
//    }

//    [Fact]
//    public void SetModelProperty()
//    {
//        ContentItem content = CustomerSchema.CreateContent();

//        Customer customer = content.ToModel<Customer>();
//        customer.Lastname = "Doe";
//        customer.Firstname = "John";
//        customer.Slug = new SlugField("my-path");

//        Assert.Equal("Doe", content.GetValue<string>("Lastname"));
//        Assert.Equal("John", content.GetValue<string>("Firstname"));
//        //Assert.Equal(123, content.GetInteger("Value"));
//        Assert.Equal("my-path", content.GetValue<string>("Slug"));
//    }

//    //[Fact]
//    //public void IgnoreNotMappedPropery()
//    //{
//    //    ContentItem content = CustomerSchema.CreateContent();

//    //    Customer customer = content.ToCustomer();
//    //    customer.Lastname = "Doe";
//    //    customer.Remark = "NotMapped";
//    //}
//}
