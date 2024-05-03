using System;
using System.Diagnostics;
using Castle.MicroKernel.Resolvers;
using Castle.Windsor;
using CastleItWillBe.Models;
using NUnit.Framework;
using static CastleItWillBe.Models.CircularFixed;
using static CastleItWillBe.Models.Circular;
using Component = Castle.MicroKernel.Registration.Component;
#pragma warning disable IDE0059
#pragma warning disable S125
#pragma warning disable S1481
#pragma warning disable S2699

namespace CastleItWillBe
{
    [TestFixture]
    public class LetsTalkAboutInjection
    {
        [Test]
        public void CreateCircularServiceAWithoutDi()
        {
            var a =
                new CircularServiceA(
                    new CircularServiceB(
                        new CircularServiceC(
                            new CircularServiceA(null))));
        }

        [Test]
        public void ResolveCircularWithError()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Register(Component.For<CircularServiceA>().ImplementedBy<CircularServiceA>().LifestyleSingleton());
            container.Register(Component.For<CircularServiceB>().ImplementedBy<CircularServiceB>().LifestyleTransient());
            container.Register(Component.For<CircularServiceC>().ImplementedBy<CircularServiceC>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.Resolve<CircularServiceA>();

            // clean up, application exits
            container.Dispose();
        }


        private CircularServiceAFixed _instance;
        [Test]
        public void CreateCircularServiceAFixedWithoutDi()
        {
            //CircularServiceAFixed Instance;

            //CircularServiceAFixed Instance =
            _instance =
                new CircularServiceAFixed(
                    new CircularServiceBFixed(
                        new CircularServiceCFixed(new Lazy<CircularServiceAFixed>(() => _instance)
                            )));
            Debug.WriteLine($"Lazy: {_instance.B.C.A.Value.GetType().Name}\t{_instance.B.C.A.Value.GetHashCode()}");
        }


        [Test]
        public void ResolveCircular()
        {
            // application starts...
            var container = new WindsorContainer();

            // register rest of component(s)
            container.Register(Component.For<CircularFixed.CircularServiceAFixed>().ImplementedBy<CircularFixed.CircularServiceAFixed>().LifestyleTransient());
            container.Register(Component.For<CircularFixed.CircularServiceBFixed>().ImplementedBy<CircularFixed.CircularServiceBFixed>().LifestyleTransient());
            container.Register(Component.For<CircularFixed.CircularServiceCFixed>().ImplementedBy<CircularFixed.CircularServiceCFixed>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.Resolve<CircularFixed.CircularServiceAFixed>();

            //new Lazy<SendMailServices.ISendMailService>(() => container.Resolve<SendMailServices.ISendMailService>());

            // clean up, application exits
            container.Dispose();
        }

        [Test]
        public void ResolveCircular_WorkingNow()
        {
            // application starts...
            var container = new WindsorContainer();

            // this is required to allow lazy to be loaded automatically
            container.Register(Component.For<ILazyComponentLoader>().ImplementedBy<LazyOfTComponentLoader>());

            // register rest of component(s)
            container.Register(Component.For<CircularFixed.CircularServiceAFixed>().ImplementedBy<CircularFixed.CircularServiceAFixed>().LifestyleTransient());
            container.Register(Component.For<CircularFixed.CircularServiceBFixed>().ImplementedBy<CircularFixed.CircularServiceBFixed>().LifestyleTransient());
            container.Register(Component.For<CircularFixed.CircularServiceCFixed>().ImplementedBy<CircularFixed.CircularServiceCFixed>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.Resolve<CircularFixed.CircularServiceAFixed>();

            // clean up, application exits
            container.Dispose();
        }
    }
}