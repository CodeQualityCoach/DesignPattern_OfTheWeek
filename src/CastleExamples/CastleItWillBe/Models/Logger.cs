using System;
using System.Diagnostics;

namespace CastleItWillBe.Models
{
    public class Logger
    {
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
    }
}