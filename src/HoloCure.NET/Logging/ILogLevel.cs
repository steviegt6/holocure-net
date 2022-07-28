using System;

namespace HoloCure.NET.Logging
{
    public interface ILogLevel
    {
        string Name { get; }
        
        ConsoleColor? ForegroundColor { get; }

        ConsoleColor? BackgroundColor { get; }
    }
}