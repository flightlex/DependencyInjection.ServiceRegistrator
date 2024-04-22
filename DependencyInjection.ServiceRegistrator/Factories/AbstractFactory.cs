using DependencyInjection.ServiceRegistrator.Interfaces;
using System;

namespace DependencyInjection.ServiceRegistrator.Helpers
{
    public sealed class AbstractFactory<T>(Func<T> factory) : IFactory<T>
    {
        public T Create() => factory();
    }
}
