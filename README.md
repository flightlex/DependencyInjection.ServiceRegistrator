# DependencyInjection.ServiceRegistrator
### Super tiny library for registrating your services and factory instance types by using only attributes

## How to setup services
If you want to apply interface with implementation (service) type, you can try this approach.
```csharp
public interface IDataAccess
{
	string GetData();
}

[ServiceRegistration(typeof(IDataAccess), typeof(DataAccess))]
public class DataAccess : IDataAccess
{
	public string GetData()
	{
		return "Data sample";
	}
}


```

Otherwise if you dont need a corresponding interface, you can just skip it.
```csharp
[ServiceRegistration(typeof(DataAccess))]
public class DataAccess : IDataAccess
{
	public string GetData()
	{
		return "Data sample";
	}
}
```

## How to setup factories
```csharp
[ServiceRegistration(typeof(TesterClass), Type = ServiceType.Factory)]
public class TesterClass
{
    public TesterClass()
    {
        TestString = Guid.NewGuid().ToString();
    }

    public string TestString { get; }
}
```

## How to register the services
In this example i will use `IHostBuilder` from the `Microsoft.Extensions.Hosting` package, but you can just use `IServiceCollection` instance, example lower.
```csharp
var hostBuilder = Host.CreateDefaultBuilder();
ServiceRegistrator.Register(hostBuilder);

var host = hostBuilder.Build();
```

```csharp
var serviceCollection = new ServiceCollection();
ServiceRegistrator.Register(serviceCollection);

var serviceProvider = serviceCollection.BuildServiceProvider();
```

`ServiceRegistrator` is a public static class that obtains every type of the calling assembly, then it takes only types that have `ServiceRegistration`. This is being implemented using reflection.

## How to use factories
```csharp
var factory = services.GetRequiredService<IFactory<TesterClass>>();

// creating 5 new instances using factory pattern
for (int i = 0; i < 5; i++)
	Console.WriteLine(factory.Create().TestString);
```
