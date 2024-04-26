using System;
using System.Diagnostics;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace CastleItWillBe.Models
{
    public class MyMailStrategy : IMailStrategy
    {
        public IDemoLogger Logger { get; set; }

        public MyMailStrategy(IDemoLogger logger)
        {
            Logger = logger;
            Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
            Debug.WriteLine($"\t{logger.GetType().Name}\t{logger.GetHashCode()}");
        }
    }

    public interface IMailStrategy
    {
        IDemoLogger Logger { get; set; }
    }

    public class ConsoleLogger : IDemoLogger
    {
        public ConsoleLogger()
        {
            Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
        }

        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class TraceLogger : IDemoLogger
    {
        public TraceLogger()
        {
            Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
        }
        public void Log(string message)
        {
            Trace.WriteLine(message);
        }
    }

    public interface IDemoLogger
    {
        void Log(string message);
    }

    public class RepositoriesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ConsoleLogger>());
            //container.Register(Classes.FromThisAssembly()
            //    .Where(Component.IsInSameNamespaceAs<King>())
            //    .WithService.DefaultInterfaces()
            //    .LifestyleTransient());
        }
    }
}