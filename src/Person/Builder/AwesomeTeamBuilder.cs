using System.Linq;

namespace Person.Builder
{
    internal class AwesomeTeamBuilder : IDefineMembersNow, ICanOnlyBuild
    {
        private Person _boss;
        private Person[] _member;
        private bool _worksAtCustomerSite;
        private bool _allowHomeOffice;

        public IDefineMembersNow AssignBoss(Person boss)
        {
            _boss = boss;
            return this;
        }

        public IDefineMembersNow AddMember(Person member)
        {
            if (_member is null) _member = new Person[0];
            _member = _member.Append(member).ToArray();
            return this;
        }

        IDefineMembersNow IDefineMembersNow.AddMember(Person member)
        {
            if (_member is null) _member = new Person[0];
            _member = _member.Append(member).ToArray();
            return this;
        }

        ICanOnlyBuild IDefineMembersNow.AllowHomeOffice()
        {
            _allowHomeOffice = true;
            _worksAtCustomerSite = false;
            return this;
        }

        ICanOnlyBuild IDefineMembersNow.WorksAtCustomerSite()
        {
            _worksAtCustomerSite = true;
            _allowHomeOffice = false;
            return this;
        }

        Team ICanOnlyBuild.Build()
        {
            if (_boss is null) throw new System.Exception("Boss is not assigned");

            return new Team(_boss, _member, _worksAtCustomerSite, _allowHomeOffice);
        }
    }

    internal interface IDefineBossFirst
    {
        IDefineMembersNow AssignBoss(Person boss);
        IDefineMembersNow AddMember(Person member);
    }

    internal interface IDefineMembersNow
    {
        IDefineMembersNow AddMember(Person member);
        ICanOnlyBuild AllowHomeOffice();
        ICanOnlyBuild WorksAtCustomerSite();
    }

    internal interface ICanOnlyBuild
    {
        Team Build();
    }
}
