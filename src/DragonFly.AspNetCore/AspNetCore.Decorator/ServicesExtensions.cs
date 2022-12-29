// Copyright (c) usercode
// https://github.com/usercode/AspNetCore.Decorator
// MIT License

using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.Decorator;

public static class ServicesExtensions
{
    public static IServiceCollection Decorate<TService, TDecorator>(this IServiceCollection services)
       where TDecorator : TService
    {
        return Decorate(services, typeof(TService), typeof(TDecorator));
    }

    public static IServiceCollection Decorate(this IServiceCollection services, Type serviceType, Type decoratorType)
    {
        if (TryDecorate(services, serviceType, decoratorType) == false)
        {
            throw new Exception("There aren't services to decorate!");
        }

        return services;
    }

    public static bool TryDecorate<TService, TDecorator>(this IServiceCollection services)
        where TDecorator : TService
    
    {
        return TryDecorate(services, typeof(TService), typeof(TDecorator));
    }

    public static bool TryDecorate(this IServiceCollection services, Type serviceType, Type decoratorType)
    {
        //find services to decorate
        IEnumerable<ServiceDescriptorEntry> descriptors = services
                                                                .Select((x, pos) => new ServiceDescriptorEntry(pos, x))
                                                                .Where(entry => entry.ServiceDescriptor.ServiceType == serviceType)
                                                                .ToArray();

        if (descriptors.Any() == false)
        {
            return false;
        }

        //decorate found services
        foreach (ServiceDescriptorEntry entry in descriptors)
        {
            //ImplementationInstance
            if (entry.ServiceDescriptor.ImplementationInstance != null)
            {
                services[entry.Position] = ServiceDescriptor.Describe(
                                                                serviceType,
                                                                provider => ActivatorUtilities.CreateInstance(provider, decoratorType, entry.ServiceDescriptor.ImplementationInstance),
                                                                entry.ServiceDescriptor.Lifetime);
            }
            //ImplementationType
            else if (entry.ServiceDescriptor.ImplementationType != null)
            {
                Type decoratedServiceType = new DecoratedType(serviceType);

                services.Add(new ServiceDescriptor(decoratedServiceType, entry.ServiceDescriptor.ImplementationType, entry.ServiceDescriptor.Lifetime));

                services[entry.Position] = ServiceDescriptor.Describe(
                                                                serviceType,
                                                                provider => ActivatorUtilities.CreateInstance(provider, decoratorType, provider.GetRequiredService(decoratedServiceType)),
                                                                entry.ServiceDescriptor.Lifetime);
            }
            //ImplementationFactory
            else if (entry.ServiceDescriptor.ImplementationFactory != null)
            {
                var factory = entry.ServiceDescriptor.ImplementationFactory;

                services[entry.Position] = ServiceDescriptor.Describe(
                                                                serviceType,
                                                                provider => ActivatorUtilities.CreateInstance(provider, decoratorType, factory(provider)),
                                                                entry.ServiceDescriptor.Lifetime);
            }
            else
            {
                throw new Exception("Unknown service descriptor!");
            }
        }

        return true;
    }
}
