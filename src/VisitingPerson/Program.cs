using System;
using Person.Element;

namespace Person
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var p1 = new Element.Person(Guid.NewGuid(), "Thomas 1", "Ley", new DateTime(1980, 2, 20));
            var p2 = new Element.Person(Guid.NewGuid(), "Thomas 2", "Ley", new DateTime(1980, 2, 20));
            var p3 = new Element.Person(Guid.NewGuid(), "Thomas 3", "Ley", new DateTime(1980, 2, 20));
            var p4 = new Element.Person(Guid.NewGuid(), "Thomas 4", "Ley", new DateTime(1980, 2, 20));

            var a1 = new Abteilung
            {
                Name = "Abteilung 1",
                OrgElements = new IOrgElement[] { p1, p2 }
            };

            IOrgElement a2 = new Abteilung
            {
                Name = "Abteilung 2",
                OrgElements = new IOrgElement[] { a1, p3, p4 }
            };


            //var v = new Visitor.JustPrintTheNameVisitor();

            var v = new Visitor.SaveAsXml();
            //v.Do(a2);

            a2.Accept(v);

            Console.ReadKey();
        }
    }
}
