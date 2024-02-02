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
}