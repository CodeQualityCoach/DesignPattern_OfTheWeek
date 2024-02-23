namespace Person.Builder
{
    internal interface IHrBuilder
    {
        void AssignBoss(Person boss);
        void AddMember(Person member);
        void AllowHomeOffice();
        void WorksAtCustomerSite();
    }
}