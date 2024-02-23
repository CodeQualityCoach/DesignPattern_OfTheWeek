using System;
using PdfTools.Logging;

namespace PdfTools.Actions
{
    public class LoggingDecoratorCommand : ICommand
    {
        private readonly ICommand _command;
        private readonly ILogger _logger;

        public LoggingDecoratorCommand(ICommand command, ILogger logger)
        {
            _command = command;
            _logger = logger;
        }

        public void Execute(string[] args)
        {
            _logger.Debug($"Executing command: {_command.GetType().Name} with args {string.Join(",", args)}");

            try
            {
                _command.Execute(args);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "An error occurred");
            }
        }

        public bool CanExecute(string[] context)
        {
            return _command.CanExecute(context);
        }
    }
}