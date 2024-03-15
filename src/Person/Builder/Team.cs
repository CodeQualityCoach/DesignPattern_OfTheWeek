using Person.Builder;

namespace Person.Builder
{
    internal class Team
    {
        public Person Boss { get; }
        public Person[] Member { get; }
        public bool WorksAtCustomerSite { get; }
        public bool AllowHomeOffice { get; }

        public Team(Person boss, Person[] member, bool worksAtCustomerSite, bool allowHomeOffice)
        {
            Boss = boss;
            Member = member;
            WorksAtCustomerSite = worksAtCustomerSite;
            AllowHomeOffice = allowHomeOffice;
        }
    }
}