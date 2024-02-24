using System;
using PdfTools.Logging;

namespace PdfTools.Actions
{
    public class LoggingDecorator : ICommand
    {
        private readonly ICommand _command;
        private readonly IPdfToolsLogger _pdfToolsLogger;

        public LoggingDecorator(ICommand command, IPdfToolsLogger pdfToolsLogger)
        {
            _command = command;
            _pdfToolsLogger = pdfToolsLogger;
        }

        public void Execute(string[] args)
        {
            try
            {
                _pdfToolsLogger.Debug($"Executing command {_command.GetType().Name}");
                _command.Execute(args);
            }
            catch (Exception ex)
            {
                _pdfToolsLogger.Error(ex, $"Error executing command {_command.GetType().Name}");
                throw;
            }
        }

        public bool CanExecute(string[] context)
        {
            return _command.CanExecute(context);
        }
    }
}