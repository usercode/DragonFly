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
        
    }

    [Fact]
    public void GetModelProperty()
    {
        ContentSchema schema = new ContentSchema();
        schema.AddString("Lastname");
        schema.AddString("Firstname");
        schema.AddBool("IsActive");
        schema.AddInteger("Value");
        schema.AddSlug("Slug");

        ContentItem content = schema.CreateContent();
        content.SetString("Lastname", "Doe");
        content.SetString("Firstname", "John");
        content.SetBool("IsActive", true);
        content.SetInteger("Value", 123);
        content.SetSlug("Slug", "my-path");

        Customer customer = content.ToModel<Customer>();

        Assert.Equal("Doe", customer.Lastname);
        Assert.Equal("John", customer.Firstname);
        Assert.True(customer.IsActive);
        Assert.Equal(123, customer.Value);
        Assert.Equal("my-path", customer.Slug.Value);
    }

    [Fact]
    public void SetModelProperty()
    {
        ContentSchema schema = new ContentSchema();
        schema.AddString("Lastname");
        schema.AddString("Firstname");
        schema.AddBool("IsActive");
        schema.AddInteger("Value");
        schema.AddSlug("Slug");

        ContentItem content = schema.CreateContent();

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
        ContentSchema schema = new ContentSchema();
        schema.AddString("Firstname");

        ContentItem content = schema.CreateContent();

        Customer customer = content.ToModel<Customer>();
        customer.Lastname = "Doe";

        Assert.Throws<Exception>(() => content.GetString("Lastname"));
    }
}
