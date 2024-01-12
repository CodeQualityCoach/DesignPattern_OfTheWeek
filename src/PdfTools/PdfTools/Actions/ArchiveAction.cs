namespace PdfTools.Actions
{
    class ArchiveAction : IAction
    {
        public void Do(string[] args)
        {
            var archiver = new PdfArchiver();
            archiver.Archive(args[1]);
            archiver.SaveAs(args[2]);
        }

        public void GetHelp()
        {
            throw new System.NotImplementedException();
        }
    }
}