using Person.Element;
using System;

namespace Person.Visitor
{
    public class SaveAsXml : IVisitor
    {
        public void Do(Abteilung element)
        {
            Console.WriteLine($"<element name='{element.Name}' />");
        }

        public void Do(Element.Person element)
        {
            Console.WriteLine($"<element vorname='{element.FirstName}' />");
        }
    }

    public interface IVisitor
    {
        void Do(Abteilung element);
        void Do(Element.Person element);
    }

}