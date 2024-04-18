using System;
using Person.Element;

namespace Person.Visitor
{
    public class JustPrintTheNameVisitor : IVisitor
    {
        public void Do(IOrgElement element)
        {
            Console.WriteLine(element.Name);
        }

        public void Do(Abteilung element)
        {
            Do((IOrgElement)element);
        }

        public void Do(Element.Person element)
        {
            Do((IOrgElement)element);
        }
    }
}