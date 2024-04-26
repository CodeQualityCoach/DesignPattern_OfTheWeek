using System.Diagnostics;

namespace CastleItWillBe.Models
{
    public class Circular
    {
        public class CircularServiceA
        {
            public CircularServiceA(CircularServiceB b)
            {
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{b.GetType().Name}\t{b.GetHashCode()}");
            }
        }

        public class CircularServiceB
        {
            public CircularServiceB(CircularServiceC c)
            {
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{c.GetType().Name}\t{c.GetHashCode()}");
            }
        }

        public class CircularServiceC
        {
            public CircularServiceC(CircularServiceA a)
            {
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{a.GetType().Name}\t{a.GetHashCode()}");
            }
        }
    }
}