using Bogus;
using DragonFly.Content;
using DragonFly.Core;
using System;
using System.Threading.Tasks;

namespace DragonFly.SampleData
{
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
                                            .CreateItem()
                                            .SetReference("Brand", new ContentItem(Guid.Parse(""), schemaBrand))
                                            .SetString("Name", "ProductA")
                                            .SetBool("IsAvailable", true)
                                            .SetFloat("Price", 9.99)
                                            .SetAsset("Image", new Asset())
                                            .SetTextArea("Description", "...");

            //await dataStorage.CreateAsync(schemaProduct);


            var schemas = new Faker<ContentItem>("de")
                                .CustomInstantiator(x => schemaBrand.CreateItem())
                                .FinishWith((f, c) => 
                                { 
                                    c.SetString("Name", f.Vehicle.Manufacturer());
                                    c.SetSlug("Slug", c.GetField<StringField>("Name").Value.ToSlug());
                                    c.SetTextArea("Description", f.Rant.Review());
                                })
                                .GenerateLazy(100);

            var product = new Faker<ContentItem>("de")
                               .CustomInstantiator(x => schemaProduct.CreateItem())
                               .FinishWith((f, c) =>
                               {
                                   c.SetString("Name", f.Vehicle.Model());
                                   c.SetSlug("Slug", c.GetField<StringField>("Name").Value.ToSlug());
                                   c.SetBool("IsAvailable", f.Random.Bool(0.8f));
                                   c.SetTextArea("Description", f.Rant.Review());
                               })
                               .GenerateLazy(100);

            
        }
    }
}
