using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Person
{
    [TestFixture]
    internal class Test
    {
        [Test]
        public void Foo()
        {
            var p = new Person() { FirstName = "Thomas", LastName = "Ley" };
            var q = new Person() { FirstName = "Raja", LastName = "Ley" };
            var r = new Person() { FirstName = "Lukas", LastName = "Feld" };
            var a = new Abteilung("Raccoons");
            var b = new Abteilung("Pandas");

            b.HrElements.Add(p);
            b.HrElements.Add(q);
            a.HrElements.Add(r);
            a.HrElements.Add(b);

        }
    }
}
