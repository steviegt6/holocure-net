using System;

namespace HoloCure.Core.Logging
{
    public static class LogLevels
    {
        public readonly record struct LogLevel(string Name, ConsoleColor? BackgroundColor, ConsoleColor? ForegroundColor) : ILogLevel;
        
        public static LogLevel Verbose { get; } = new("Verbose", null, ConsoleColor.DarkGray);
        
        public static LogLevel Debug { get; } = new("Debug", null, ConsoleColor.Gray);
        
        public static LogLevel Info { get; } = new("Info", null, ConsoleColor.White);
        
        public static LogLevel Warning { get; } = new("Warning", null, ConsoleColor.Yellow);
        
        public static LogLevel Error { get; } = new("Error", null, ConsoleColor.Red);
        
        public static LogLevel Fatal { get; } = new("Fatal", null, ConsoleColor.DarkRed);
    }
}