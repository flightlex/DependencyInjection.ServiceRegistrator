using DependencyInjection.ServiceRegistrator.Enums;
using System;

namespace DependencyInjection.ServiceRegistrator.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class ServiceRegistrationAttribute : Attribute
    {
        public ServiceRegistrationAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public ServiceRegistrationAttribute(Type serviceType, Type implementationType)
        {
            ServiceType = serviceType;
            ImplementationType = implementationType;
        }

        public Type ServiceType { get; }
        public Type? ImplementationType { get; }

        public ServiceType Type { get; set; } = Enums.ServiceType.Singleton;
    }
}
