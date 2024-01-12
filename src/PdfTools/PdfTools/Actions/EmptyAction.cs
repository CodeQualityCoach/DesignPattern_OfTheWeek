using System;

namespace PdfTools.Actions
{
    class EmptyAction : IAction
    {
        public void Do(string[] args)
        {
            Console.WriteLine("No action found for args");
        }

        public void GetHelp()
        {
            // print global pdftools help
        }
    }
}