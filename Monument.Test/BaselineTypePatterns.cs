using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monument.SimpleInjector;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monument.Test
{
    [TestClass]
    public class BaselineTypePatterns
    {
        public static Type GetType<T>(params Type[] types) 
            where T : class => Get<T>(types).GetType();
        
        public static T Get<T>(params Type[] types)
            where T : class =>new TypePatternRegistrationConvention()
                .Register(types, new ContainerAdapter(new Container()))
                .ToRuntimeContainer()
                .Resolve<T>();
        
        [TestMethod]
        public void BasicRegistration()
        => Assert.AreEqual(typeof(SimpleImplementation1), 
            GetType<ISimpleInterface>(
                typeof(SimpleImplementation1)));

        [TestMethod]
        public void BasicDecoratorRegistration()
         => Assert.AreEqual(typeof(SimpleDecorator),
             GetType<ISimpleInterface>(
                 typeof(SimpleImplementation1), 
                 typeof(SimpleDecorator)));

        [TestMethod]
        public void BasicCompositeRegistration()
         => Assert.AreEqual(typeof(SimpleComposite),
             GetType<ISimpleInterface>(
                 typeof(SimpleComposite),
                 typeof(SimpleImplementation1),
                 typeof(SimpleImplementation2),
                 typeof(SimpleImplementation3)));

        [TestMethod]
        public void OpenGenericRegistration()
         => Assert.AreEqual(typeof(OpenGenericImplementation1<string>),
             GetType<IGenericInterface<string>>(
                 typeof(OpenGenericImplementation1<>)));

        [TestMethod]
        public void OpenGenericDecoratorRegistration()
         => Assert.AreEqual(typeof(OpenGenericDecorator<string>),
             GetType<IGenericInterface<string>>(
                 typeof(OpenGenericDecorator<>),
                 typeof(OpenGenericImplementation1<>)));

        [TestMethod]
        public void OpenGenericCompositeRegistration()
         => Assert.AreEqual(typeof(OpenGenericComposite<string>),
             GetType<IGenericInterface<string>>(
                 typeof(OpenGenericComposite<>),
                 typeof(OpenGenericImplementation1<>),
                 typeof(OpenGenericImplementation2<>),
                 typeof(OpenGenericImplementation3<>)));
    }

    public class SimpleImplementation1 : ISimpleInterface{}
    public class SimpleImplementation2 : ISimpleInterface{}
    public class SimpleImplementation3 : ISimpleInterface{}
    public class OpenGenericImplementation1<T> : IGenericInterface<T>{}
    public class OpenGenericImplementation2<T> : IGenericInterface<T>{}
    public class OpenGenericImplementation3<T> : IGenericInterface<T>{}
    public class ClosedGenericImplementation1: IGenericInterface<SimpleImplementation1>{}
    public class ClosedGenericImplementation2 : IGenericInterface<SimpleImplementation1>{}
    public class ClosedGenericImplementation3 : IGenericInterface<SimpleImplementation1>{}
    public class SimpleComposite : ISimpleInterface{public SimpleComposite(IEnumerable<ISimpleInterface> d) { }}
    public class OpenGenericComposite<T> : IGenericInterface<T>{public OpenGenericComposite(IEnumerable<IGenericInterface<T>> d) { }}
    public class ClosedGenericComposite : IGenericInterface<SimpleImplementation1>{public ClosedGenericComposite(IEnumerable<IGenericInterface<SimpleImplementation1>> d) { }}
    public class SimpleDecorator : ISimpleInterface{public SimpleDecorator(ISimpleInterface d) { }}
    public class OpenGenericDecorator<T> : IGenericInterface<T>{public OpenGenericDecorator(IGenericInterface<T> d) { }}
    public class ClosedGenericDecorator : IGenericInterface<SimpleImplementation1>{public ClosedGenericDecorator(IGenericInterface<SimpleImplementation1> d) { }}
    public interface ISimpleInterface{}
    public interface IGenericInterface<T>{}
}
