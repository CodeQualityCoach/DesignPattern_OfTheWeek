using System;
using Person.Builder;

namespace Person
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var p = Person.Create(Guid.NewGuid(), "Thomas", "Ley", new DateTime(1980, 2, 20));


            var b = new TeamBuilder();

            b.AssignBoss(p);
            b.AddMember(p);
            b.AddMember(p);
            b.WorksAtCustomerSite();

            var t = b.Build();
        }
    }
}
