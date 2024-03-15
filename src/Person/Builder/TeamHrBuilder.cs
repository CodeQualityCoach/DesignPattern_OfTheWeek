using System.Linq;

namespace Person.Builder
{
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