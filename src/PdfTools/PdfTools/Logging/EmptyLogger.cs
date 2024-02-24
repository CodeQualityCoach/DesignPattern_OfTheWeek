using System;

namespace PdfTools.Logging
{
    internal class EmptyPdfToolsLogger : IPdfToolsLogger
    { 
        public void Debug(string message) { }
        public void Error(Exception ex, string message) { }
    }
}