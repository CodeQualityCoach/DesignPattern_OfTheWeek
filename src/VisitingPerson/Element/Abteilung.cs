using System;
using System.Linq;
using Person.Visitor;

namespace Person.Element
{
    public class Abteilung : IOrgElement
    {
        // name
        public string Name { get; set; }

        public void Accept(IVisitor visitor)
        {
            visitor.Do(this);
            //OrgElements.ToList().ForEach(visitor.Do);
            OrgElements.ToList().ForEach(x=>x.Accept(visitor));
        }

        // persons
        public IOrgElement[] OrgElements { get; set; } = Array.Empty<IOrgElement>();

    }
}