using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CastleItWillBe.Models;
using NUnit.Framework;
#pragma warning disable S1481
#pragma warning disable S2699
#pragma warning disable IDE0059

namespace CastleItWillBe
{
    [TestFixture]
    public class LetsTalkAboutDifferentBehaviour
    {
        [Test]
        public void FirstComeFirstServe()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleSingleton());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleSingleton());

            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.EmptyMailService>().LifestyleSingleton());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleSingleton());

            var mailService1 = container.Resolve<SendMailServices.ISendMailService>();
        }

        [Test]
        public void OverridePreviousRegistrationWithIsDefault()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleSingleton());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleSingleton());

            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.EmptyMailService>().LifestyleSingleton());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleSingleton().IsDefault());

            var mailService1 = container.Resolve<SendMailServices.ISendMailService>();
        }

        [Test]
        public void OverridePreviousRegistrationWithIsFallback()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleSingleton());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleSingleton());

            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.EmptyMailService>().LifestyleSingleton().IsFallback());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleSingleton());

            var mailService1 = container.Resolve<SendMailServices.ISendMailService>();
        }
    }
}