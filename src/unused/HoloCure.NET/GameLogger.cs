using System;
using System.Collections.Generic;
using HoloCure.Logging;
using HoloCure.Logging.Levels;
using HoloCure.NET.Exceptions;

namespace HoloCure.NET
{
    public static class GameLogger
    {
        public record SourceLogger(ILogger Logger, string Source) : ILogger
        {
            public IList<ILogWriter> Writers => throw new NotImplementedException();
            
            public void Log(string message, ILogLevel level) {
                Logger.Log($"[{Source}] {message}", level);
            }
            
            public void LogLiteral(string message, ILogLevel level) {
                Logger.LogLiteral(message, level);
            }

            public void LogOnce(string message, ILogLevel level) {
                Logger.LogOnce($"[{Source}] {message}", level);
            }

            public void LogLiteralOnce(string message, ILogLevel level) {
                Logger.LogLiteralOnce(message, level);
            }
            
            void IDisposable.Dispose() {
                throw new NotImplementedException();
            }
        }

        private static ILogger? Logger;

        public static void InitializeLogger(ILogger logger) {
            if (Logger is not null) throw new LoggerAlreadyInitializedException();
            Logger = logger;
        }

        public static SourceLogger MakeLogger(string source) {
            return new SourceLogger(Logger ?? throw new LoggerNotInitializedException(), source);
        }

        public static void DisposeLogger() {
            if (Logger is null) throw new LoggerNotInitializedException();
            Logger.Dispose();
        }
    }
}