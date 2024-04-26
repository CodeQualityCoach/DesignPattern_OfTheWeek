using System.Diagnostics;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CastleItWillBe.Models;
using NUnit.Framework;

namespace CastleItWillBe
{
    [TestFixture]
    public class LetsTalkAboutScopes
    {
        [Test]
        public void SingletonAndTransient()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Register(Component.For<IDemoLogger>().ImplementedBy<ConsoleLogger>().LifestyleTransient());
            container.Register(Component.For<IMailStrategy>().ImplementedBy<MyMailStrategy>().LifestyleTransient());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.Resolve<IMailStrategy>();
            _ = container.Resolve<IMailStrategy>();

            _ = container.Resolve<SendMailServices.ISendMailService>();
            _ = container.Resolve<SendMailServices.ISendMailService>();

            // clean up, application exits
            container.Dispose();
        }

        [Test]
        public void AddingScopes_NewScopeNewLogger()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Register(Component.For<IDemoLogger>().ImplementedBy<ConsoleLogger>().LifestyleScoped());
            container.Register(Component.For<IMailStrategy>().ImplementedBy<MyMailStrategy>().LifestyleTransient());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            var s = container.BeginScope();
            var t = container.BeginScope();
            _ = container.Resolve<IMailStrategy>();
            _ = container.Resolve<IMailStrategy>();
            t.Dispose();
            s.Dispose();

            s = container.BeginScope();
            Debug.WriteLine("Here comes a new Logger");
            _ = container.Resolve<SendMailServices.ISendMailService>();
            _ = container.Resolve<SendMailServices.ISendMailService>();
            s.Dispose();

            // clean up, application exits
            container.Dispose();
        }

        [Test]
        public void ResolveScoped()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Register(Component.For<IDemoLogger>().ImplementedBy<ConsoleLogger>().LifestyleScoped());
            container.Register(Component.For<IMailStrategy>().ImplementedBy<MyMailStrategy>().LifestyleTransient());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.ResolveScoped<IMailStrategy>();
            _ = container.ResolveScoped<IMailStrategy>();

            _ = container.ResolveScoped<SendMailServices.ISendMailService>();
            _ = container.ResolveScoped<SendMailServices.ISendMailService>();

            // clean up, application exits
            container.Dispose();
        }

        [Test]
        public void ResolveScoped_Mixed()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Register(Component.For<IDemoLogger>().ImplementedBy<ConsoleLogger>().LifestyleSingleton());
            container.Register(Component.For<IMailStrategy>().ImplementedBy<MyMailStrategy>().LifestyleScoped());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.ResolveScoped<IMailStrategy>();
            _ = container.ResolveScoped<IMailStrategy>();

            _ = container.ResolveScoped<SendMailServices.ISendMailService>();
            _ = container.ResolveScoped<SendMailServices.ISendMailService>();

            // clean up, application exits
            container.Dispose();
        }

        [Test]
        public void ResolveScoped_WhatIsHappeningHere()
        {
            // application starts...
            var container = new WindsorContainer();

            // adds and configures all components using WindsorInstallers from executing assembly
            container.Register(Component.For<IDemoLogger>().ImplementedBy<ConsoleLogger>().LifestyleScoped());
            container.Register(Component.For<IMailStrategy>().ImplementedBy<MyMailStrategy>().LifestyleSingleton());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.ResolveScoped<IMailStrategy>();
            _ = container.ResolveScoped<IMailStrategy>();

            _ = container.ResolveScoped<SendMailServices.ISendMailService>();
            _ = container.ResolveScoped<SendMailServices.ISendMailService>();

            // clean up, application exits
            container.Dispose();
        }
    }

    public static class WindsorExtensions
    {
        public static T ResolveScoped<T>(this WindsorContainer container)
        {
            Debug.WriteLine("--- Starting a new Scope ---");
            using (container.BeginScope())
            {
                var result = container.Resolve<T>();
                Debug.WriteLine("--- Scope closed ---");
                return result;
            }
        }
    }

}