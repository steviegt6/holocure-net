using System;
using HoloCure.Logging;
using HoloCure.Logging.Levels;

namespace HoloCure.NET.Desktop.Logging.Writers
{
    public class ConsoleLogWriter : ILogWriter
    {
        public void Log(string message, ILogLevel level) {
            Console.ResetColor();
            if (level is IConsoleLogLevel consoleLevel) {
                if (consoleLevel.ForegroundColor is not null) Console.ForegroundColor = consoleLevel.ForegroundColor.Value;
                if (consoleLevel.BackgroundColor is not null) Console.BackgroundColor = consoleLevel.BackgroundColor.Value;
            }
            Console.WriteLine(message);
        }

        void IDisposable.Dispose() { }
    }
}