namespace Person.Element
{
    public class Abteilung : IOrgElement
    {
        // name
        public string Name { get; set; }
        // persons
        public IOrgElement[] OrgElements { get; set; }
    }
}