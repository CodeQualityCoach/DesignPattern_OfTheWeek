using System;
using NLog;

namespace PdfTools.Logging.NLog
{
    public class LoggerFactory
    {
        public IPdfToolsLogger CreateLogger(Type type)
        {
            return new NLogAdapter(LogManager.GetLogger(type.Name));
        }
    }
}