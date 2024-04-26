using System;
using System.Diagnostics;

namespace CastleItWillBe.Models
{
    public class CircularFixed
    {
        public class CircularServiceAFixed
        {
            public CircularServiceBFixed B { get; }

            public CircularServiceAFixed(CircularServiceBFixed b)
            {
                B = b;
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{b.GetType().Name}\t{b.GetHashCode()}");
            }
        }

        public class CircularServiceBFixed
        {
            public CircularServiceCFixed C { get; }

            public CircularServiceBFixed(CircularServiceCFixed c)
            {
                C = c;
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{c.GetType().Name}\t{c.GetHashCode()}");
            }
        }

        public class CircularServiceCFixed
        {
            public Lazy<CircularServiceAFixed> A { get; }

            public CircularServiceCFixed(Lazy<CircularServiceAFixed> a)
            {
                A = a;
                Debug.WriteLine($"Id of {this.GetType().Name}\t{GetHashCode()}");
                Debug.WriteLine($"\t{a.GetType().Name}\t{a.GetHashCode()}");
                //Debug.WriteLine($"\t{a.Value.GetType().Name}\t{a.GetHashCode()}");
            }
        }
    }
}