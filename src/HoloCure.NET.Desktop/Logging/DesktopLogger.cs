using System;
using System.Collections.Generic;
using System.Linq;
using HoloCure.NET.Logging;

namespace HoloCure.NET.Desktop.Logging
{
    public class DesktopLogger : ILogger
    {
        public IList<ILogWriter> Writers { get; }

        public DesktopLogger(params ILogWriter[] writers) {
            Writers = writers.ToList();
        }

        public void Log(string message, ILogLevel level) {
            string msg = $"[{DateTime.Now:hh:mm:ss}] [{level.Name}] {message}";

            foreach (ILogWriter writer in Writers) {
                writer.Log(msg, level);
            }
        }

        public void Dispose() {
            foreach (ILogWriter writer in Writers) {
                writer.Dispose();
            }
        }
    }
}