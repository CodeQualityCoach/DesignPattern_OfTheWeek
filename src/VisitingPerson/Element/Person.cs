using System;

namespace Person.Element
{
    public class Person : IOrgElement
    {
        public Person(Guid uuid, string firstName, string name, DateTime birthday)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Birthday = birthday;
            Id = uuid;
        }

        public Person()
        {
            
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; }
    }
}
