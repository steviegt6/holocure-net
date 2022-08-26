using System;
using System.Collections.Generic;
using System.Linq;
using HoloCure.Logging;
using HoloCure.Logging.Levels;

namespace HoloCure.NET.Desktop.Logging
{
    public class DesktopLogger : ILogger
    {
        public IList<ILogWriter> Writers { get; }

        protected readonly List<string> LogOnceMessages = new();

        public DesktopLogger(params ILogWriter[] writers) {
            Writers = writers.ToList();
        }

        public void Log(string message, ILogLevel level) {
            LogLiteral($"[{DateTime.Now:hh:mm:ss}] [{level.Name}] {message}", level);
        }
        
        public void LogLiteral(string message, ILogLevel level) {
            foreach (ILogWriter writer in Writers) {
                writer.Log(message, level);
            }
        }

        public void LogOnce(string message, ILogLevel level) {
            if (LogOnceMessages.Contains(message)) return;
            LogOnceMessages.Add(message);
            Log(message, level);
        }

        public void LogLiteralOnce(string message, ILogLevel level) {
            if (LogOnceMessages.Contains(message)) return;
            LogOnceMessages.Add(message);
            LogLiteral(message, level);
        }

        void  IDisposable.Dispose() {
            GC.SuppressFinalize(this);
            foreach (ILogWriter writer in Writers) {
                writer.Dispose();
            }
        }
    }
}