using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monument.Conventions;
using Monument.Types;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using Monument.Types.Generic;
using Monument.Types.Utility;
using Monument.Types.FactoryDecoration;
using Monument.Types.AbstractClasses;
using Monument.Conventions.Settings;
using Monument.Types.Records;
using Monument.Types.PrioritizedDecoration;
using Monument.DotNetContainer;
using DI = Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Monument.Test
{
    [Ignore]
    [TestClass]
    public class DotNetContainerTypePatternRegistrationConventionTests
    {
        public static Type GetType<T>(params Type[] types) where T : class
        {
            return Get<T>(types)?.GetType() ?? throw new ActivationException(); 
        }

        public static T Get<T>(params Type[] types) where T : class
        {
            var serviceCollection = new DI.ServiceCollection();
            var adapter = new DotNetRegisterTimeContainerAdapter(serviceCollection);
            var convention = new TypePatternRegistrationConvention(adapter);
            convention.Register(types);

            var runtimeAdapter = new DotNetRuntimeContainerAdapter(serviceCollection.BuildServiceProvider());
            return runtimeAdapter.Resolve<T>();
        }

        public static T Get<T>(RegistrationConventionSettings settings, params Type[] types) where T : class
        {
            var serviceCollection = new DI.ServiceCollection();
            var adapter = new DotNetRegisterTimeContainerAdapter(serviceCollection);
            var convention = new TypePatternRegistrationConvention(adapter);
            convention.Register(types, settings);

            var runtimeAdapter = new DotNetRuntimeContainerAdapter(serviceCollection.BuildServiceProvider());
            return runtimeAdapter.Resolve<T>();
        }

        [TestMethod]
        public void BasicRegistration()
        => Assert.AreEqual(typeof(SimpleImplementation1),
            GetType<ISimpleInterface>(
                typeof(SimpleImplementation1)));

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void RecordsAreNotRegistered()
        => Assert.AreEqual(typeof(ExampleRecord),
            GetType<ExampleRecord>(typeof(ExampleRecord)));

        [TestMethod]
        public void BasicDecoratorRegistration()
        {
            var type = GetType<ISimpleInterface>(typeof(SimpleImplementation1), typeof(SimpleDecorator));

            Assert.AreEqual(typeof(SimpleDecorator), type);
        }

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
         => Assert.AreEqual(typeof(OpenGenericNode1<string>),
             GetType<IGeneric<string>>(
                 typeof(OpenGenericNode1<>)));

        [TestMethod]
        public void GetTypeKeyReturnsCorrectSignature() =>
            Assert.AreEqual(typeof(IGeneric<>), typeof(IGeneric<string>).ToTypeKey());

        [TestMethod]
        public void OpenGenericDecoratorRegistration()
         => Assert.AreEqual(typeof(OpenGenericDecorator<string>),
             GetType<IGeneric<string>>(
                 typeof(OpenGenericDecorator<>),
                 typeof(OpenGenericNode1<>)));

        [TestMethod]
        [ExpectedException(typeof(ActivationException))]
        public void AbstractClassIsNotRegistered() => GetType<AbstractService>();

        [TestMethod]
        public void MixedDecorators()
        {
            var instance = Get<IGeneric<Animal>>(
                typeof(OpenGenericDecorator<>),
                typeof(ClosedGenericDecorator),
                typeof(ClosedGenericNode1));

            var decorator1 = instance as OpenGenericDecorator<Animal> ?? (instance as ClosedGenericDecorator).DecoratedComponent as OpenGenericDecorator<Animal>;
            var decorator2 = instance as ClosedGenericDecorator ?? (instance as OpenGenericDecorator<Animal>).DecoratedComponent as ClosedGenericDecorator;

            Assert.IsNotNull(decorator1);
            Assert.IsNotNull(decorator2);
        }

        [TestMethod]
        public void MixedCollectionOfOpenAndClosedImplementations()
        {
            var instances = Get<IEnumerable<IGeneric<Animal>>>(
                typeof(OpenGenericNode1<>),
                typeof(ClosedGenericNode1));

            var instance2s = Get<IEnumerable<IGeneric<SimpleImplementation2>>>(
                typeof(OpenGenericNode1<>),
                typeof(ClosedGenericNode1));

            Assert.AreEqual(1, instance2s.Count());
            Assert.AreEqual(2, instances.Count());
        }

        [TestMethod]
        public void OpenGenericCompositeAroundMixedCollectionOfOpenAndClosedImplementations() =>
            Assert.AreEqual(typeof(OpenGenericComposite<Animal>), GetType<IGeneric<Animal>>(
                typeof(OpenGenericComposite<>),
                typeof(OpenGenericNode1<>),
                typeof(ClosedGenericNode1)));

        [TestMethod]
        public void CompositeAroundMixedCollectionOfOpenAndClosedImplementations()
        {
            var composite = Get<IGeneric<Animal>>(
                typeof(LocalClosedGenericComposite),
                typeof(OpenGenericNode1<>),
                typeof(OpenGenericNode2<>),
                typeof(ClosedGenericNode1),
                typeof(ClosedGenericNode2),
                typeof(ClosedGenericNode3),
                typeof(ClosedGenericNode4));


            Assert.AreEqual(typeof(LocalClosedGenericComposite), composite.GetType());
            Assert.AreEqual(4, (composite as LocalClosedGenericComposite).NodeCount);
        }

        [Ignore] // Not supported by SimpleInjector.
        [TestMethod]
        public void FactoryDecoratorsAreStillConsideredDecorators()
        {
            var decoratedSecretService = Get<ISecretService>(
                typeof(SecretServiceImplementation),
                typeof(SecretServiceFactoryDecorator));

            Assert.AreEqual(typeof(SecretServiceFactoryDecorator), decoratedSecretService.GetType());
        }

        [TestMethod]
        public void CompositeAroundClosedGenericTypes()
        {
            var genericComposite = Get<IGeneric<Animal>>(typeof(ClosedGenericNode1), typeof(ClosedGenericNode2), typeof(LocalClosedGenericComposite));

            Assert.AreEqual(typeof(LocalClosedGenericComposite), genericComposite.GetType());
        }

        [TestMethod]
        public void DecoratedCompositeAroundClosedGenericTypes()
        {
            var genericComposite = Get<IGeneric<Animal>>(typeof(ClosedGenericNode1), typeof(ClosedGenericNode2), typeof(LocalClosedGenericComposite), typeof(ClosedGenericDecorator));

            Assert.AreEqual(typeof(ClosedGenericDecorator), genericComposite.GetType());
        }

        [TestMethod]
        public void DecorationOrderFromAttributesIsPreserved()
        {
            var decoratableInterface = Get<IDecoratableInterface>(
                typeof(LowPriorityDecorator),
                typeof(HighPriorityDecorator),
                typeof(NonPrioritizedDecorator),
                typeof(UnderlyingImplementation)
                );

            Assert.AreEqual(typeof(HighPriorityDecorator), decoratableInterface.GetType());
            var firstDecorator = decoratableInterface as HighPriorityDecorator;

            Assert.AreEqual(typeof(NonPrioritizedDecorator), firstDecorator.DecoratedInterface.GetType());
            var secondDecorator = firstDecorator.DecoratedInterface as NonPrioritizedDecorator;

            Assert.AreEqual(typeof(LowPriorityDecorator), secondDecorator.DecoratedInterface.GetType());
            var thirdDecorator = secondDecorator.DecoratedInterface as LowPriorityDecorator;

            Assert.AreEqual(typeof(UnderlyingImplementation), thirdDecorator.DecoratedInterface.GetType());
        }

        [TestMethod]
        public void DecorationOrderFromAttributesIsPreservedInDifferentOrder()
        {
            var decoratableInterface = Get<IDecoratableInterface>(
                typeof(HighPriorityDecorator),
                typeof(NonPrioritizedDecorator),
                typeof(LowPriorityDecorator),
                typeof(UnderlyingImplementation)
                );

            Assert.AreEqual(typeof(HighPriorityDecorator), decoratableInterface.GetType());
            var firstDecorator = decoratableInterface as HighPriorityDecorator;

            Assert.AreEqual(typeof(NonPrioritizedDecorator), firstDecorator.DecoratedInterface.GetType());
            var secondDecorator = firstDecorator.DecoratedInterface as NonPrioritizedDecorator;

            Assert.AreEqual(typeof(LowPriorityDecorator), secondDecorator.DecoratedInterface.GetType());
            var thirdDecorator = secondDecorator.DecoratedInterface as LowPriorityDecorator;

            Assert.AreEqual(typeof(UnderlyingImplementation), thirdDecorator.DecoratedInterface.GetType());
        }

        [TestMethod, Ignore]
        public void WhereDoesTheDecoratorHide()
        {
            var instance = Get<IGeneric<Animal>>(
                typeof(LocalClosedGenericComposite),
                typeof(OpenGenericNode1<>),
                typeof(ClosedGenericDecorator),
                typeof(ClosedGenericNode1));

            Assert.IsTrue(true);

            // that's a problem
        }

        [TestMethod]
        public void OpenGenericCompositeRegistration()
         => Assert.AreEqual(typeof(OpenGenericComposite<string>),
             GetType<IGeneric<string>>(
                 typeof(OpenGenericComposite<>),
                 typeof(OpenGenericNode1<>),
                 typeof(OpenGenericNode2<>)));

        [TestMethod]
        public void ClosedAndOpenGenericListRegistration()
        {
            Assert.AreEqual(3, Get<IEnumerable<IGeneric<Animal>>>(
                typeof(OpenGenericNode1<>),
                typeof(ClosedGenericNode1),
                typeof(ClosedGenericNode2),
                typeof(ClosedGenericNode3),
                typeof(ClosedGenericNode4)).Count());

            Assert.AreEqual(2, Get<IEnumerable<IGeneric<Animal>>>(
                typeof(OpenGenericNode1<>),
                typeof(ClosedGenericNode2),
                typeof(ClosedGenericNode3)).Count());

            Assert.AreEqual(3, Get<IEnumerable<IGeneric<Block>>>(
                typeof(OpenGenericNode1<>),
                typeof(ClosedGenericNode1),
                typeof(ClosedGenericNode2),
                typeof(ClosedGenericNode3),
                typeof(ClosedGenericNode4)).Count());
        }

        [TestMethod]
        public void ClosedGenericRegistration()
        {
            Assert.IsNotNull(Get<IGeneric<Animal>>(
                typeof(ClosedGenericNode1),
                typeof(ClosedGenericNode4)));
            Assert.IsNotNull(Get<IGeneric<Block>>(
                typeof(ClosedGenericNode1),
                typeof(ClosedGenericNode4)));
        }

        [TestMethod]
        public void ClosedGenericListRegistration() =>
            Assert.AreEqual(2, Get<IEnumerable<IGeneric<Animal>>>(
                typeof(ClosedGenericNode1),
                typeof(ClosedGenericNode2)).Count());

        //[TestMethod]
        //public void ClosedGenericAdapterRegistration() =>
        //    Assert.AreEqual(typeof(ClosedGenericAdapter), GetType<IGeneric<Animal>>(
        //        typeof(ClosedGenericAdapter),
        //        typeof(ClosedGenericImplementation4)));
    }

    public class LocalClosedGenericComposite : IGeneric<Animal>
    {
        private readonly IEnumerable<IGeneric<Animal>> genericNodes;

        public int NodeCount => genericNodes.Count();

        public LocalClosedGenericComposite(IEnumerable<IGeneric<Animal>> genericNodes)
        {
            this.genericNodes = genericNodes;
        }


    }
}
