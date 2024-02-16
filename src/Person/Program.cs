using System;

namespace Person
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var p = Person.Create(Guid.NewGuid(), "Thomas", "Ley", new DateTime(1980, 2, 20));


        }
    }
}
