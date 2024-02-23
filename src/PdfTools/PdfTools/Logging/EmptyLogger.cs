using System;

namespace PdfTools.Logging
{
    internal class EmptyLogger : ILogger
    { 
        public void Debug(string message) { }
        public void Error(Exception ex, string message) { }
    }
}