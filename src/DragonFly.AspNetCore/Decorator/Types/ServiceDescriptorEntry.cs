// Copyright (c) usercode
// https://github.com/usercode/AspNetCore.Decorator
// MIT License

using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Decorator;

/// <summary>
/// ServiceDescriptorEntry
/// </summary>
struct ServiceDescriptorEntry
{
    public ServiceDescriptorEntry(int posititon, ServiceDescriptor serviceDescriptor)
    {
        Position = posititon;
        ServiceDescriptor = serviceDescriptor;
    }

    /// <summary>
    /// ServiceDescriptor
    /// </summary>
    public ServiceDescriptor ServiceDescriptor { get; }

    /// <summary>
    /// Position
    /// </summary>
    public int Position { get; }
}
