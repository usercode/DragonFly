// See https://aka.ms/new-console-template for more information
using DragonFly.Fields.BlockField;
using DragonFly.Fields.BlockField.Blocks;
using DragonFly.Fields.BlockField.Storage.Json;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

BlockFieldManager.Default.RegisterElement<BlockElement>();
BlockFieldManager.Default.RegisterElement<ImageElement>();

Document d = new Document();
BlockElement b1 = new BlockElement();
b1.Columns.Add(new ColumnElement() { Element = new ImageElement() });

d.Blocks.Add(b1);
d.Blocks.Add(new BlockElement());

string json = JsonConvert.SerializeObject(d, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.Auto, SerializationBinder = new JsonDiscriminatorBinder() });

var r = JsonConvert.DeserializeObject<Document>(json, 
    new JsonSerializerSettings() 
{ 
    TypeNameHandling = TypeNameHandling.Auto,
    SerializationBinder = new JsonDiscriminatorBinder() 
});

Console.WriteLine("");