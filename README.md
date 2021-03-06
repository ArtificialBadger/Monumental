# Monument
A IoC container agnostic dependency injection registration engine that prioritizes using conventions on type-patterns over other registration methods.

[![Build Test and Publish](https://github.com/ArtificialBadger/Monumental/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ArtificialBadger/Monumental/actions/workflows/dotnet.yml)

## Usage (SimpleInjector)
```
var adapter = new SimpleInjectorAdapter(container);
var convention = new TypePatternRegistrationConvention(adapter);
convention.Register(Assembly.GetExecutingAssembly().GetTypes());
convention.RegisterFactory(adapter);
```

## Attributes
`[Transient]` - Default, only needed if you want to explicitly declare your lifestyle. Creates a new instance from the DI container for every resolution.

`[Singleton]` - Single instance is created and shared by all dependers.

`[Scoped]` - New instance created per scope. For ASP.Net applications, this will be one instance per api request context.

`[Ignore]` - This class will not be automatically registered, useful for prototyping or in certain GoF patterns such as Chain of Responsibility.

## NuGet
3 packages are publically available

Monumental

Monumental.SimpleInjector

Monumental.DotNetContainer
