using System;

namespace PdfTools.Actions
{
    public class EmptyDecoratorCommand : ICommand
    {
        private readonly ICommand _command;

        public EmptyDecoratorCommand(ICommand command)
        {
            _command = command;
        }

        public void Execute(string[] args)
        {
            try
            {
                _command.Execute(args);
            }
            catch (Exception ex)
            {
                // Do nothing
            }
        }

        public bool CanExecute(string[] context)
        {
            return _command.CanExecute(context);
        }
    }
}