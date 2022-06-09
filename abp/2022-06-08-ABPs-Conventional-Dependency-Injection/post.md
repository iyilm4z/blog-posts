# ABP's Conventional Dependency Injection

The ABP framework is developed by forcing it to apply best practices and the SOLID principles whenever possible. Therefore, the ABP framework uses [Dependency Injection(DI)](https://en.wikipedia.org/wiki/Dependency_injection), which is the most widely used technique to implement the last principle [Dependency Inversion Principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle) of the [SOLID](https://en.wikipedia.org/wiki/SOLID) principles. But with a slightly different and smart approach than the usual way. This approach is called Conventional Dependency Injection. To understand what ABP does differently, let's first take a look at what the usual DI approach looks like in .NET Core.

> Important: I'm continuing this blog assuming you already know and use DI. Therefore, if you don't know DI yet, I recommend you learn DI first and then read this blog.

The usual approach consists of 3 steps.

1. Definition: Define an interface named `IUserService` to do our operations with the User, and implement it from a class named `UserService`.

```csharp
public interface IUserService {
}

public class UserService : IUserService {
}
```

2. Registration: Register the `IUserService` service to .NET Core's DI to use it throughout the application via DI.

```csharp
var builder = WebApplication.CreateBuilder(args);

// ...

builder.Services.AddTransient<IUserService, UserService>();

// ...

var app = builder.Build();
```

3. Resolving: Use `IUserService` in `UserController`.

```csharp
public class UserController : Controller {
    private readonly IUserService _userService;

    UserController(IUserService userService) {
        _userService = userService
    }
}
```

As you can see, the usual approach is quite simple. But as your project grows and becomes more complex, this simplicity leads to simple mistakes and a waste of time. How? It happens like this, developers often accidentally skip the 2nd step registration, due to work pressure. Then an error is thrown in the 3rd step resolving. Because the DI cannot create the instance of the service whose registration is forgotten. Finally, the developer stops the application, handles the 3rd step registration, and rebuilds the application again. This is a complete waste of time and It's frustrating because it's so repetitive.

We mostly use DI just like in the usual approach example. We usually have an interface and a class implements it. We register this pair in DI and then resolve the service. And each service usually has lifetime scopes that are Transient, Singleton, and Scoped.

```csharp
builder.Services.AddTransient<IFooService, FooService>();
builder.Services.AddSingleton<IBarService, BarService>();
builder.Services.AddScoped<IBazService, BazService>();
```

So when we have enough information, is it possible to make the usual DI smarter, less cumbersome, and less error-prone? If you are an ABP user the answer is yes.

The DI infrastructure of ABP is based on an approach called Conventional Dependency Injection. The infrastructure provides us with 3 interfaces associated with DI lifetimes, namely ITransidentDependecy, ISingetonDependecy, and IScopedDependecy. And ABP wants us to mark(related to [Marker Interface Pattern](https://en.wikipedia.org/wiki/Marker_interface_pattern)) each service with these interfaces.

**ITransientDependency** = *AddTransient()*

**ISingletonDependency** = *AddSingleton()*

**IScopedDependency** = *AddScoped()*

The marking process makes a convention. This convention automates the 2nd step registration in the usual approach example, for you. In other words, it merges the 1st step definition and the 2nd step registration. Therefore, it helps you to save development time and write less error-prone code.

Let's take a look at what ABP's Conventional Dependency Injection approach, which consists of only 2 steps, looks like.

1. **Definition & Registration**: In addition to the 1st step definition in the usual approach example, let's implement the `ITransidentDependecy` interface of ABP by `UserService` in this step.

```csharp
public interface IUserService {
}

public class UserService : IUserService, ITransidentDependecy {
}
```

2. Resolving: And now we can use `IUserService` in `UserController`.

```csharp

public class UserController : Controller {
    private readonly IUserService _userService;

    UserController(IUserService userService) {
        _userService = userService
    }
}
```

So how does ABP do this? The answer is Reflection!

When bootstrapping an application, ABP finds all the [ABP modules](https://docs.abp.io/en/abp/latest/Module-Development-Basics) in the application. And each ABP module also holds the assembly information it is defined in. ABP will filter out the types within each ABP module's assembly that implement the convention interfaces and register them in .NET Core's DI, as in the 2nd step registration in the usual approach example.

A simple and smart solution! Right?