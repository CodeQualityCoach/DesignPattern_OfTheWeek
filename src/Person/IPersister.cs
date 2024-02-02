namespace Person
{
    public interface IPersister
    {
        void Load(string path);
        void Save(string path);
    }
}