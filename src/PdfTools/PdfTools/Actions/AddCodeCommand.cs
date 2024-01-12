using System;

namespace PdfTools.Actions
{
    class AddCodeCommand : ICommand
    {
        public void Execute(string[] args)
        {
            var enhancer = new PdfCodeEnhancer(args[1]);

            enhancer.AddTextAsCode(args[2]);

            if (args.Length == 4)
                enhancer.SaveAs(args[3]);
            else
                enhancer.SaveAs(args[1]);
        }
        public bool CanExecute(string[] context)
        {
            var action = context[0];
            return string.Equals(action, "addcode", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}