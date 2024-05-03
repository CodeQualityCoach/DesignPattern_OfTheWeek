using System.Diagnostics;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CastleItWillBe.Models;
using NUnit.Framework;
#pragma warning disable S2699

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
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleTransient());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleTransient());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.Resolve<MailStrategies.IMailStrategy>();
            _ = container.Resolve<MailStrategies.IMailStrategy>();

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
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleScoped());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleTransient());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            var s = container.BeginScope();
            var t = container.BeginScope();
            _ = container.Resolve<MailStrategies.IMailStrategy>();
            _ = container.Resolve<MailStrategies.IMailStrategy>();
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
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleScoped());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleTransient());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.ResolveScoped<MailStrategies.IMailStrategy>();
            _ = container.ResolveScoped<MailStrategies.IMailStrategy>();

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
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleSingleton());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleScoped());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.ResolveScoped<MailStrategies.IMailStrategy>();
            _ = container.ResolveScoped<MailStrategies.IMailStrategy>();

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
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleScoped());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleSingleton());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleTransient());

            // instantiate and configure root component and all its dependencies and their dependencies and...
            _ = container.ResolveScoped<MailStrategies.IMailStrategy>();
            _ = container.ResolveScoped<MailStrategies.IMailStrategy>();

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