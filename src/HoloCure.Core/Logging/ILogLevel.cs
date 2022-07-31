using System;

namespace HoloCure.Core.Logging
{
    public interface ILogLevel
    {
        string Name { get; }
        
        ConsoleColor? ForegroundColor { get; }

        ConsoleColor? BackgroundColor { get; }
    }
}