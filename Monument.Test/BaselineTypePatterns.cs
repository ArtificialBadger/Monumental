using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monument.SimpleInjector;
using Monument.Conventions;
using Monument.Types;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using Monument.Types.GenericComposite;
using Monument.Types.Utility;

namespace Monument.Test
{
    [TestClass]
    public class BaselineTypePatterns
    {
        public static Type GetType<T>(params Type[] types)
            where T : class => Get<T>(types).GetType();

        public static T Get<T>(params Type[] types)
            where T : class => ((new TypePatternRegistrationConvention()
                .Register(types, new SimpleInjectorAdapter(new Container()))) as Monument.Containers.IConvertableContainer)
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
        public void GetTypeKeyReturnsCorrectSignature() =>
            Assert.AreEqual(typeof(IGenericInterface<>), typeof(IGenericInterface<string>).ToTypeKey());

        [TestMethod]
        public void OpenGenericDecoratorRegistration()
         => Assert.AreEqual(typeof(OpenGenericDecorator<string>),
             GetType<IGenericInterface<string>>(
                 typeof(OpenGenericDecorator<>),
                 typeof(OpenGenericImplementation1<>)));

        [TestMethod]
        public void OpenGenericDecoratorWithClosedDecoratorRegistration()
        {
            var instance = Get<IGenericInterface<SimpleImplementation1>>(
                typeof(OpenGenericDecorator<>),
                typeof(ClosedGenericDecorator),
                typeof(ClosedGenericImplementation1));

            var decorator1 = instance as OpenGenericDecorator<SimpleImplementation1> ?? (instance as ClosedGenericDecorator).Inner as OpenGenericDecorator<SimpleImplementation1>;
            var decorator2 = instance as ClosedGenericDecorator ?? (instance as OpenGenericDecorator<SimpleImplementation1>).Inner as ClosedGenericDecorator;

            Assert.IsNotNull(decorator1);
            Assert.IsNotNull(decorator2);
        }

        [TestMethod]
        public void MixedCollectionOfOpenAndClosedImplementations()
        {
            var instances = Get<IEnumerable<IGenericInterface<SimpleImplementation1>>>(
                typeof(OpenGenericImplementation1<>),
                typeof(ClosedGenericImplementation1));

            var instance2s = Get<IEnumerable<IGenericInterface<SimpleImplementation2>>>(
                typeof(OpenGenericImplementation1<>),
                typeof(ClosedGenericImplementation1));

            Assert.AreEqual(1, instance2s.Count());
            Assert.AreEqual(2, instances.Count());
        }

        [TestMethod]
        public void OpenGenericCompositeAroundMixedCollectionOfOpenAndClosedImplementations() =>
            Assert.AreEqual(typeof(OpenGenericComposite<SimpleImplementation1>), GetType<IGenericInterface<SimpleImplementation1>>(
                typeof(OpenGenericComposite<>),
                typeof(OpenGenericImplementation1<>),
                typeof(ClosedGenericImplementation1)));

        [TestMethod]
        public void CompositeAroundMixedCollectionOfOpenAndClosedImplementations()
        {
            var composite = Get<IGeneric<Animal>>(
                typeof(ClosedGenericComposite),
                typeof(OpenGenericNode1<>),
                typeof(OpenGenericNode2<>),
                typeof(ClosedGenericNode2),
                typeof(ClosedGenericNode4));


            Assert.AreEqual(typeof(ClosedGenericComposite), composite.GetType());
            Assert.AreEqual(4, (composite as ClosedGenericComposite).NodeCount);
        }

        [TestMethod]
        public void CompositeAroundClosedGenericTypes()
        {
            var genericComposite = Get<IGeneric<Animal>>(typeof(ClosedGenericNode1), typeof(ClosedGenericNode2), typeof(ClosedGenericComposite));

            Assert.AreEqual(typeof(ClosedGenericComposite), genericComposite.GetType());
        }

        [TestMethod]
        public void DecoratedCompositeAroundClosedGenericTypes()
        {
            var genericComposite = Get<IGeneric<Animal>>(typeof(ClosedGenericNode1), typeof(ClosedGenericNode2), typeof(ClosedGenericComposite), typeof(ClosedGenericCompositeDecorator));

            Assert.AreEqual(typeof(ClosedGenericCompositeDecorator), genericComposite.GetType());
        }

        [TestMethod, Ignore]
        public void WhereDoesTheDecoratorHide()
        {
            var instance = Get<IGenericInterface<SimpleImplementation1>>(
                typeof(ClosedGenericComposite),
                typeof(OpenGenericImplementation1<>),
                typeof(ClosedGenericDecorator),
                typeof(ClosedGenericImplementation1));

            Assert.IsTrue(true);

            // that's a problem
        }

        [TestMethod]
        public void OpenGenericCompositeRegistration()
         => Assert.AreEqual(typeof(OpenGenericComposite<string>),
             GetType<IGenericInterface<string>>(
                 typeof(OpenGenericComposite<>),
                 typeof(OpenGenericImplementation1<>),
                 typeof(OpenGenericImplementation2<>),
                 typeof(OpenGenericImplementation3<>)));

        [TestMethod]
        public void ClosedAndOpenGenericListRegistration()
        {
            Assert.AreEqual(2, Get<IEnumerable<IGenericInterface<SimpleImplementation2>>>(
                typeof(OpenGenericImplementation1<>),
                typeof(ClosedGenericImplementation1),
                typeof(ClosedGenericImplementation4)).Count());
            Assert.AreEqual(2, Get<IEnumerable<IGenericInterface<SimpleImplementation1>>>(
                typeof(OpenGenericImplementation1<>),
                typeof(ClosedGenericImplementation1),
                typeof(ClosedGenericImplementation4)).Count());
        }

        [TestMethod]
        public void ClosedGenericRegistration()
        {
            Assert.IsNotNull(Get<IGenericInterface<SimpleImplementation2>>(
                typeof(ClosedGenericImplementation1),
                typeof(ClosedGenericImplementation4)));
            Assert.IsNotNull(Get<IGenericInterface<SimpleImplementation1>>(
                typeof(ClosedGenericImplementation1),
                typeof(ClosedGenericImplementation4)));
        }

        [TestMethod]
        public void ClosedGenericListRegistration() =>
            Assert.AreEqual(2, Get<IEnumerable<IGenericInterface<SimpleImplementation1>>>(
                typeof(ClosedGenericImplementation1),
                typeof(ClosedGenericImplementation2)).Count());

        [TestMethod]
        public void ClosedGenericAdapterRegistration() =>
            Assert.AreEqual(typeof(ClosedGenericAdapter), GetType<IGenericInterface<SimpleImplementation1>>(
                typeof(ClosedGenericAdapter),
                typeof(ClosedGenericImplementation4)));
    }
}
