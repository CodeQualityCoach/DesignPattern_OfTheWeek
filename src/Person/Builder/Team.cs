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
    // implement the builder pattern for class Team
    // Methods “AddMember”, “AssignBoss”, “AllowHomeOffice”, “WorksAtCustomerSite” 
    internal class TeamBuilder
    {
        public Team Build()
        {
            throw new System.NotImplementedException();
        }
    }   
}
