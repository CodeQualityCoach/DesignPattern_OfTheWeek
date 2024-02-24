using System;
using NLog;

namespace PdfTools.Logging.NLog
{
    public class NLogAdapter : IPdfToolsLogger
    {
        private readonly ILogger _adaptee;

        public NLogAdapter(ILogger adaptee)
        {
            _adaptee = adaptee;
        }

        public void Debug(string message)
        {
           _adaptee.Debug(message);
        }

        public void Error(Exception ex, string message)
        {
            _adaptee.Error(ex, message);
        }
    }
}
