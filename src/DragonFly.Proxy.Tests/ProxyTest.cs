// Copyright (c) usercode
// https://github.com/usercode/DragonFly
// MIT License

using DragonFly.Proxy.Tests.Models;
using Xunit;

namespace DragonFly.Proxy.Tests;

public class ProxyTest
{    
    public ProxyTest()
    {
        CustomerSchema = new ContentSchema("Customer");
        CustomerSchema.AddString("Lastname");
        CustomerSchema.AddString("Firstname");
        CustomerSchema.AddBool("IsActive");
        CustomerSchema.AddInteger("Value");
        CustomerSchema.AddSlug("Slug");

        ProxyTypeManager.Default.Add<Customer>(CustomerSchema);
    }

    private ContentSchema CustomerSchema { get; }

    [Fact]
    public void GetSuitableModelType()
    {
        ContentItem content = CustomerSchema.CreateContent();
        content.SetString("Lastname", "Doe");
        content.SetString("Firstname", "John");

        IContentModel model = content.ToModel();

        Assert.True(model is Customer);
    }

    [Fact]
    public void GetModelProperty()
    {
        ContentItem content = CustomerSchema.CreateContent();
        content.Id = Guid.NewGuid();
        content.SetString("Lastname", "Doe");
        content.SetString("Firstname", "John");
        content.SetBool("IsActive", true);
        content.SetInteger("Value", 123);
        content.SetSlug("Slug", "my-path");

        Customer customer = content.ToModel<Customer>();

        Assert.Equal(content.Id, customer.Id);
        Assert.Equal("Doe", customer.Lastname);
        Assert.Equal("John", customer.Firstname);
        Assert.True(customer.IsActive);
        Assert.Equal(123, customer.Value);
        Assert.Equal("my-path", customer.Slug.Value);
    }

    [Fact]
    public void SetModelProperty()
    {
        ContentItem content = CustomerSchema.CreateContent();

        Customer customer = content.ToModel<Customer>();
        customer.Lastname = "Doe";
        customer.Firstname = "John";
        customer.IsActive = true;
        customer.Value = 123;
        customer.Slug = new SlugField("my-path");

        Assert.Equal("Doe", content.GetString("Lastname"));
        Assert.Equal("John", content.GetString("Firstname"));
        Assert.True(content.GetBool("IsActive"));
        Assert.Equal(123, content.GetInteger("Value"));
        Assert.Equal("my-path", content.GetSlug("Slug"));
    }

    [Fact]
    public void IgnoreNotMappedPropery()
    {
        ContentItem content = CustomerSchema.CreateContent();

        Customer customer = content.ToModel<Customer>();
        customer.Lastname = "Doe";
        customer.Remark = "NotMapped";
    }
}
