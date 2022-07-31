using System;

namespace HoloCure.Core.Logging
{
    public interface ILogWriter : IDisposable
    {
        void Log(string message, ILogLevel level);
    }
}