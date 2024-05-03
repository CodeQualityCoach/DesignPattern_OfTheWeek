using System.Diagnostics;

namespace CastleItWillBe.Models
{
    public class MailStrategies
    {
        public class MyMailStrategy : IMailStrategy
        {
            public Logger.IDemoLogger Logger { get; set; }

            public MyMailStrategy(Logger.IDemoLogger logger)
            {
                Logger = logger;
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{logger.GetType().Name}\t{logger.GetHashCode()}");
            }
        }

        public interface IMailStrategy
        {
            Logger.IDemoLogger Logger { get; set; }
        }
    }
}