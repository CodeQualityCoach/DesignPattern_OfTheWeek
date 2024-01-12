using System;

namespace PdfTools.Actions
{
    public interface ICommand
    {
        void Execute(string[] context);
        bool CanExecute(string[] context);
    }

    class LogArgs : ICommand
    {
        public void Execute(string[] context)
        {
            Console.WriteLine(string.Join(", ", context));
        }

        public bool CanExecute(string[] context)
        {
            return true;
        }
    }
}