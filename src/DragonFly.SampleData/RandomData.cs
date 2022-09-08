using Bogus;
using DragonFly.Core;
using DragonFly.Storage;
using System;
using System.Threading.Tasks;

namespace DragonFly.SampleData;

public class RandomData
{
    public async Task CreateDataAsync(IDataStorage dataStorage)
    {
        //create brand schema
        ContentSchema schemaBrand = new ContentSchema("Brand")
                                            .AddString("Name")
                                            .AddSlug("Slug")
                                            .AddTextArea("Description");

        //Define schema for product
        ContentSchema schemaProduct = new ContentSchema("Product")
                                            .AddReference("Brand")
                                            .AddString("Name", options => options.IsRequired = true)
                                            .AddSlug("Slug")
                                            .AddBool("IsAvailable", options => options.DefaultValue = true)
                                            .AddFloat("Price")
                                            .AddTextArea("Description", options => options.MaxLength = 255)
                                            .AddAsset("Image")
                                            .AddArray("Attributes", options => options
                                                                                .AddString("Name")
                                                                                .AddString("Value"));

        ContentItem contentProduct = schemaProduct
                                        .CreateContentItem()
                                        .SetReference("Brand", new ContentItem(Guid.Parse(""), schemaBrand))
                                        .SetString("Name", "ProductA")
                                        .SetBool("IsAvailable", true)
                                        .SetFloat("Price", 9.99)
                                        .SetAsset("Image", new Asset())
                                        .SetText("Description", "...")
                                        .AddArrayItem("Attributes", schemaProduct, item => item
                                                                                .SetString("Name", "Size")
                                                                                .SetString("Value", "M"));

        //await dataStorage.CreateAsync(schemaProduct);


        var schemas = new Faker<ContentItem>("de")
                            .CustomInstantiator(x => schemaBrand.CreateContentItem())
                            .FinishWith((f, c) => 
                            { 
                                c.SetString("Name", f.Vehicle.Manufacturer());
                                c.SetSlug("Slug", c.GetField<StringField>("Name").Value.ToSlug());
                                c.SetText("Description", f.Rant.Review());
                            })
                            .GenerateLazy(100);

        var product = new Faker<ContentItem>("de")
                           .CustomInstantiator(x => schemaProduct.CreateContentItem())
                           .FinishWith((f, c) =>
                           {
                               c.SetString("Name", f.Vehicle.Model());
                               c.SetSlug("Slug", c.GetField<StringField>("Name").Value.ToSlug());
                               c.SetBool("IsAvailable", f.Random.Bool(0.8f));
                               c.SetText("Description", f.Rant.Review());
                           })
                           .GenerateLazy(100);

        
    }
}
