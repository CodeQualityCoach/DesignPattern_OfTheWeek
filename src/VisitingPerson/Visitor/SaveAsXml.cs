using System;

namespace Person
{
    public class SaveAsXml
    {
        public void Save(object theElement)
        {
            Console.Write($"<element name='{theElement}' />");
        }
    }
}