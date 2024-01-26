namespace Person
{
    interface IPerson : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}