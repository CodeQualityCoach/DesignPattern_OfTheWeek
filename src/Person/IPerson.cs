namespace Person
{
    public interface IPerson : IEntity
    {
        string FirstName { get; set; }
        string LastName { get; set; }
    }
}