using System;
using Person.Element;

namespace Person.Visitor
{
    public class JustPrintTheName
    {
        public void Save(IOrgElement theElement)
        {
            Console.Write(theElement.Name);
        }
    }
}