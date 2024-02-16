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
    // introduce the interface IBuilder with methods from TeamBuilder (except build)
    // implement the builder pattern for class OfficeSetup with interface IBuilder
    // Rules: AllowHomeOffice ==> 50% Number of Desks
    //        WorksAtCustomerSite ==> No Desks except Boss
    //        Boss always has a desk
    // Result: OfficeRoomSetup
    internal class OfficeSetupBuilder
    {
        public OfficeSetup Build()
        {
            throw new System.NotImplementedException();
        }
    }
}