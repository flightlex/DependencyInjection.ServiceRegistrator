using DependencyInjection.ServiceRegistrator.Attributes;
using DependencyInjection.ServiceRegistrator.Enums;
using DependencyInjection.ServiceRegistrator.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace DependencyInjection.ServiceRegistrator.General;

public static class ServiceRegistrator
{
    public static void Register(IHostBuilder hostBuilder)
    {
        var executingAssembly = Assembly.GetCallingAssembly();

        var servicesAttributes = executingAssembly
            .GetTypes()
            .AsQueryable() // querying to save some unsignificant stack memory but why not :)
            .Where(x => x.GetCustomAttribute<ServiceRegistrationAttribute>() != null)
            .Select(x => x.GetCustomAttribute<ServiceRegistrationAttribute>())
            .ToArray();

        hostBuilder.ConfigureServices((hostContext, services) =>
        {
            foreach (var serviceAttribute in servicesAttributes)
            {
                RegisterService(services, serviceAttribute);
            }
        });
    }

    private static void RegisterService(IServiceCollection collection, ServiceRegistrationAttribute attr)
    {
        switch (attr.Type)
        {
            case ServiceType.Singleton:

                if (attr.ImplementationType is not null)
                    collection.AddSingleton(attr.ServiceType, attr.ImplementationType);
                else
                    collection.AddSingleton(attr.ServiceType);

                break;

            case ServiceType.Scoped:

                if (attr.ImplementationType is not null)
                    collection.AddScoped(attr.ServiceType, attr.ImplementationType);
                else
                    collection.AddScoped(attr.ServiceType);

                break;

            case ServiceType.Transient:

                if (attr.ImplementationType is not null)
                    collection.AddTransient(attr.ServiceType, attr.ImplementationType);
                else
                    collection.AddTransient(attr.ServiceType);

                break;

            case ServiceType.Factory:
                collection.AddFactory(attr.ServiceType);
                break;

            default:
                throw new ArgumentException(nameof(attr.Type));
        }
    }
}
