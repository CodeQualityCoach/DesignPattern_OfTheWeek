namespace Person
{
    interface IStore
    {
        void Load(string path);
        void Save(string path);

    }
}