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
            ContentSchema schemaBrand = new ContentSchema();
            schemaBrand.Name = "Brand";
            schemaBrand.AddString("Name");
            schemaBrand.AddSlug("Slug");
            schemaBrand.AddTextArea("Description");

            //Define schema for product
            ContentSchema schemaProduct = new ContentSchema();
            schemaProduct.Name = "Product";
            schemaProduct.AddReference("Brand");
            schemaProduct.AddString("Name", options => options.IsRequired = true);
            schemaProduct.AddSlug("Slug");
            schemaProduct.AddBool("IsAvailable", optios => optios.DefaultValue = true);
            schemaProduct.AddFloat("Price");
            schemaProduct.AddTextArea("Description", options => options.MaxLength = 255);
            schemaProduct.AddArray("Attributes", options => options
                                                                .AddString("Name")
                                                                .AddString("Value"));

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
