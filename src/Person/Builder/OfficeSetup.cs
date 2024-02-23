namespace Person.Builder
{
    internal class OfficeSetup
    {
        public int NumberOfDesks { get; }
        public int NumberOfBossDesks { get; }

        public OfficeSetup(int numberOfDesks, int numberOfBossDesks)
        {
            NumberOfDesks = numberOfDesks;
            NumberOfBossDesks = numberOfBossDesks;
        }
    }

    // Aufgabe 2:
    // --------------------------------
    // introduce the interface IHrBuilder with methods from TeamHrBuilder (except build)

    // implement the hrBuilder pattern for class OfficeSetup with interface IHrBuilder
    // Rules: AllowHomeOffice ==> 50% Number of Desks
    //        WorksAtCustomerSite ==> No Desks except Boss
    //        Boss always has a desk
    // Result: OfficeSetup
    internal class OfficeSetupHrBuilder : IHrBuilder
    {
        private int _numberOfDesks;
        private int _numberOfBossDesks;

        public void AssignBoss(Person boss)
        {
            _numberOfBossDesks = 1;
        }

        public void AddMember(Person member)
        {
            _numberOfDesks++;
        }

        public void AllowHomeOffice()
        {
            _numberOfDesks += _numberOfDesks / 2;
        }

        public void WorksAtCustomerSite()
        {
            _numberOfDesks = _numberOfBossDesks;
        }

        public OfficeSetup Build()
        {
            return new OfficeSetup(_numberOfDesks, _numberOfBossDesks);
        }
    }

    // director class for IHrBuilder
    // Director hat einen Builder als Parameter
    // CreateCustomerTeam() which works ar customer site.
    // CreateBackendTeam, works at home office and has 5 member
    internal class BuildSetupDirector
    {
        public void CreateCustomerTeam(IHrBuilder hrBuilder)
        {
            hrBuilder.AssignBoss(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.WorksAtCustomerSite();
        }

        public void CreateBackendTeam(IHrBuilder hrBuilder)
        {
            hrBuilder.AssignBoss(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AddMember(new Person());
            hrBuilder.AllowHomeOffice();
        }
    }
}