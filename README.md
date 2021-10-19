# Monument
A IoC container agnostic dependency injection registration engine that prioritizes using conventions on type-patterns over other registration methods.

## Usage (SimpleInjector)
```
var adapter = new SimpleInjectorAdapter(container);
var convention = new TypePatternRegistrationConvention(adapter);
convention.Register(Assembly.GetExecutingAssembly().GetTypes());
convention.RegisterFactory(adapter);
```
