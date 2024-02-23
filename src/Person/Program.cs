using System;
using Person.Builder;

namespace Person
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var p = Person.Create(Guid.NewGuid(), "Thomas", "Ley", new DateTime(1980, 2, 20));

            var tb = new TeamHrBuilder();
            var ob = new OfficeSetupHrBuilder();

            var d = new BuildSetupDirector();
            d.CreateCustomerTeam(tb);
            d.CreateCustomerTeam(ob);

            var t = tb.Build();
            var o = ob.Build();



            var teamB = new AwesomeTeamBuilder()
                .AssignBoss(p)
                .AddMember(p)
                .AddMember(p)
                .AddMember(p)
                .AddMember(p)
                .AllowHomeOffice()
                .Build();
        }
    }
}
