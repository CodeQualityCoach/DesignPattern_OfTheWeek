using System;

namespace Person.Visitor
{
    public class SaveAsXml
    {
        public void Save(object theElement)
        {
            Console.Write($"<element name='{theElement}' />");
        }
    }
}