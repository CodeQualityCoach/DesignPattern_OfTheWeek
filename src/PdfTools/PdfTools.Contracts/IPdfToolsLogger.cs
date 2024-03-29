﻿using System;

namespace PdfTools.Logging
{
    public interface IPdfToolsLogger
    {
        void Debug(string message);
        //void Info(string message);
        //void Warn(string message);
        void Error(Exception ex, string message);
        //void Fatal(Exception ex, string message);
    }
}
