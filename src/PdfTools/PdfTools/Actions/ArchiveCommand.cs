using System;
using PdfTools.Observer;

namespace PdfTools.Actions
{
    class ArchiveCommand : ICommand
    {
        private readonly FileCreatedSubject _subject;

        public ArchiveCommand(FileCreatedSubject subject)
        {
            _subject = subject;
        }

        public void Execute(string[] args)
        {
            var archiver = new PdfArchiver(subject: _subject);
            archiver.Archive(args[1]);
            archiver.SaveAs(args[2]);
            _subject.SetFileCreated(args[2]);
        }
        public bool CanExecute(string[] context)
        {
            var action = context[0];
            return string.Equals(action, "archive", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}