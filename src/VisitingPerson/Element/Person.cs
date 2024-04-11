using System;

namespace Person.Element
{
    public class Person : IOrgElement
    {
        public Person(Guid uuid, string firstName, string lastName, DateTime birthday)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Birthday = birthday;
            Id = uuid;
        }

        public Person()
        {
            
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; }

        #region for later pattern use

        public void Load(string path)
        {
            // Lesen der Daten aus der Datei...
        }

        public void Save(string path)
        {
            // Speichern der Daten in die Datei...
        }

        public string ToJsonString()
        {
            // Zurückliefern der Daten als JSON-Zeichenkette...
            var jsonString = "...";
            return jsonString;
        }

        public string ToXmlString()
        {
            // Zurückliefern der Daten als XML-Zeichenkette...
            var xmlString = "...";
            return xmlString;
        }

        #endregion
    }
}
