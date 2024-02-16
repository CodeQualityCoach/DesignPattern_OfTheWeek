using System;

namespace Person
{
    /********************************************************
     * Aufgabe:
     * 
     * Factory Method implementieren
     * Business Rule: Geburtsdatum hinzufügen. Darf nicht in der Zukunft liegen
     ********************************************************/

    public class PersonId
    {
        public static implicit operator PersonId(Guid value) => new PersonId(value);

        public Guid Value { get; }

        public PersonId(Guid uuid)
        {
            Value = uuid;
        }
        public PersonId()
        {
            Value = Guid.NewGuid();
        }
    }

    public class DDDPerson
    {
        private DDDPerson(PersonId uuid, string firstName, string lastName, DateTime birthday)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Birthday = birthday;
            Id = Id;
        }

        public PersonId Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; }

        public static DDDPerson Create(Guid uuid, string firstName, string lastName, DateTime birthday)
        {
            if (firstName == null) throw new ArgumentNullException();
            if (lastName == null) throw new ArgumentNullException();
            if (birthday > DateTime.Now) throw new ArgumentOutOfRangeException("birthday cannot be in the future");

            //return new DDDPerson(new PersonId(uuid), firstName, lastName, birthday);
            return new DDDPerson(uuid, firstName, lastName, birthday);
        }





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
