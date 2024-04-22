namespace DependencyInjection.ServiceRegistrator.Interfaces
{
    public interface IFactory<T>
    {
        T Create();
    }
}
