using System;

namespace PdfTools.Actions
{
    class ArchiveCommand : ICommand
    {
        public void Execute(string[] args)
        {
            var archiver = new PdfArchiver();
            archiver.Archive(args[1]);
            archiver.SaveAs(args[2]);
        }
        public bool CanExecute(string[] context)
        {
            var action = context[0];
            return string.Equals(action, "archive", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}