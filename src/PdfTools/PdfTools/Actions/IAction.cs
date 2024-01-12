namespace PdfTools.Actions
{
    public interface IAction
    {
        void Do(string[] args);
        void GetHelp();
    }
}