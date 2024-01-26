using System.Collections.Generic;

namespace Person
{
    public class Abteilung : IHrElement
    {
        public string GetName()
        {
            return Name;
        }

        public List<IHrElement> HrElements = new List<IHrElement>();


        public Abteilung(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}