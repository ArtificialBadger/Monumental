
//  Monument

// An Open-Source, Convention Based Dependency Injection Auto-Registration Platform


// Goals
// Guide towards better DI practices, with less code (no need for IocConfig or Registrations.cs)
// Allow stronger component cohesion and reduce coupling
// Encourage use of Design Patterns (Decorator, Composite, Adapter, Factory)
// Help code writers and reviewers with Threading issues



//  Proper DI, but unnecessarey
//            container.Register<IFeatureService, FeatureService>();

//  Improper DI, 
//            container.Register<IContactService>(
//                () =>
//                {
//                    var config = container.GetInstance<IServiceConfiguration>();
//                    return new ContactService(
//                        config.EnvironmentName,
//                        container.GetInstance<IFeatureService>(),
//                        container.GetInstance<ICustomerResolver>(),
//                        container.GetInstance<TelemetryLogger>()
//                        );
//                },
//                Lifestyle.Singleton);


// Advanced 
// Captive Dependency Resolution (via IFactory<>)
// Constrained and Varient Generics
// Generic Composites
// Much more!
