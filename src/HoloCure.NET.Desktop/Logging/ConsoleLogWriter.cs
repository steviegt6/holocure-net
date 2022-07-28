using System;
using HoloCure.NET.Logging;

namespace HoloCure.NET.Desktop.Logging
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Log(string message, ILogLevel level) {
            Console.ResetColor();
            if (level.ForegroundColor is not null) Console.ForegroundColor = level.ForegroundColor.Value;
            if (level.BackgroundColor is not null) Console.BackgroundColor = level.BackgroundColor.Value;
            Console.WriteLine(message);
        }

        public void Dispose() { }
    }
}