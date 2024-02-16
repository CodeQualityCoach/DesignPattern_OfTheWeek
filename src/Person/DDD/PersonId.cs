using System;

namespace Person
{
    public class PersonId
    {
        public static implicit operator PersonId(Guid value) => new PersonId(value);

        public Guid Value { get; }

        public PersonId(Guid uuid)
        {
            Value = uuid;
        }
        public PersonId()
        {
            Value = Guid.NewGuid();
        }
    }
}