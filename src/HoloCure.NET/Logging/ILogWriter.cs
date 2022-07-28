using System;

namespace HoloCure.NET.Logging
{
    public interface ILogWriter : IDisposable
    {
        void Log(string message, ILogLevel level);
    }
}