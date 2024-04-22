using DependencyInjection.ServiceRegistrator.Helpers;
using DependencyInjection.ServiceRegistrator.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjection.ServiceRegistrator.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddFactory<TService>(this IServiceCollection services) where TService : class
        {
            services.AddTransient<TService>();
            services.AddSingleton<Func<TService>>(x => () => x.GetService<TService>()!);
            services.AddSingleton<IFactory<TService>, AbstractFactory<TService>>();

            return services;
        }

        internal static IServiceCollection AddFactory(this IServiceCollection services, Type serviceType)
        {
            return (IServiceCollection)typeof(IServiceCollectionExtensions)
                .GetMethod("AddFactory")
                .MakeGenericMethod(serviceType)
                .Invoke(serviceType, [services]);
        }
    }
}
