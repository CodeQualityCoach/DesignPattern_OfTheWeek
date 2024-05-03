using System;
using System.Diagnostics;
using System.Linq;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using CastleItWillBe.Models;
using NUnit.Framework;
#pragma warning disable IDE0038
#pragma warning disable S2699
#pragma warning disable S1481
#pragma warning disable IDE0059

namespace CastleItWillBe
{
    [TestFixture]
    public class LetsTalkAboutDesignPattern
    {
        [Test]
        public void UseSingleton()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>().LifestyleSingleton());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>().LifestyleSingleton());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().LifestyleSingleton());

            var mailService1 = container.Resolve<SendMailServices.ISendMailService>();
            var mailService2 = container.Resolve<SendMailServices.ISendMailService>();
        }

        [Test]
        public void UseDecorator()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>());
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.TraceLogger>().IsDefault());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>());

            // first come first serve. The first one will be the outermost decorator
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.BlockConfidentialMailsDecorator>());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.SendMailService>().IsFallback());
            container.Register(Component.For<SendMailServices.ISendMailService>().ImplementedBy<SendMailServices.AwesomeMailsDecorator>());

            var mailService1 = container.Resolve<SendMailServices.ISendMailService>();
            mailService1.SendMail("thomas", "this is a confidential message");
        }

        [Test]
        public void UseInterceptor()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<Logger.IDemoLogger>().ImplementedBy<Logger.ConsoleLogger>());
            container.Register(Component.For<MailStrategies.IMailStrategy>().ImplementedBy<MailStrategies.MyMailStrategy>());

            // Register LogInterceptor. why????
            container.Register(Component.For<LogInterceptor>());
            container.Register(Component.For<UppercaseParameterInterceptor>());

            container.Register(Component
                .For<SendMailServices.ISendMailService>()
                .ImplementedBy<SendMailServices.SendMailService>()
                .Interceptors<LogInterceptor>()
                .Interceptors<UppercaseParameterInterceptor>());

            var mailService1 = container.Resolve<SendMailServices.ISendMailService>();
            mailService1.SendMail("thomas", "this is a confidential message");
        }
    }

    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Debug.WriteLine($"Calling: {invocation.Method}({string.Concat(", ", invocation.Arguments.Select(x => x.GetType().Name))})");
            
            invocation.Proceed();
            
            Debug.WriteLine($"Called: {invocation.Method}: {invocation.ReturnValue}");
        }
    }

    public class UppercaseParameterInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            for (var i = 0; i < invocation.Arguments.Length; i++)
            {
                if (invocation.Arguments[i] is string)
                    invocation.Arguments[i] = ((string)invocation.Arguments[i]).ToUpper();
            }

            invocation.Proceed();
        }
    }

    public class CoolLogInterceptor : IInterceptor
    {
        private readonly Logger.IDemoLogger _logger;

        public CoolLogInterceptor(Logger.IDemoLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public void Intercept(IInvocation invocation)
        {
            _logger.Log($"Called: {invocation.Method}({string.Concat(", ", invocation.Arguments.Select(x => x.GetType().Name))})");

            invocation.Proceed();
        }
    }
}
