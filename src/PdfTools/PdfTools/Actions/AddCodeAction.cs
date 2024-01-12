namespace PdfTools.Actions
{
    class AddCodeAction : IAction
    {
        public void Do(string[] args)
        {
            var enhancer = new PdfCodeEnhancer(args[1]);

            enhancer.AddTextAsCode(args[2]);

            if (args.Length == 4)
                enhancer.SaveAs(args[3]);
            else
                enhancer.SaveAs(args[1]);
        }

        public void GetHelp()
        {
            throw new System.NotImplementedException();
        }
    }
}