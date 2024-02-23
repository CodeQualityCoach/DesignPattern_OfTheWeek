using Person.Builder;
using System.Linq;

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

    // Aufgabe 1:
    // --------------------------------
    // implement the hrBuilder pattern for class Team
    // Methods “AddMember”, “AssignBoss”, “AllowHomeOffice”, “WorksAtCustomerSite” 
    internal class TeamHrBuilder : IHrBuilder
    {
        private Person _boss;
        private Person[] _member;
        private bool _worksAtCustomerSite;
        private bool _allowHomeOffice;

        public void AddMember(Person member)
        {
            if (_member is null) _member = new Person[0];
            _member = _member.Append(member).ToArray();
        }

        public void AssignBoss(Person boss)
        {
            _boss = boss;
        }

        public void AllowHomeOffice()
        {
            _allowHomeOffice = true;
            _worksAtCustomerSite = false;
        }

        public void WorksAtCustomerSite()
        {
            _worksAtCustomerSite = true;
            _allowHomeOffice = false;
        }

        public Team Build()
        {
            if (_boss is null) throw new System.Exception("Boss is not assigned");

            return new Team(_boss, _member, _worksAtCustomerSite, _allowHomeOffice);
        }
    }
}