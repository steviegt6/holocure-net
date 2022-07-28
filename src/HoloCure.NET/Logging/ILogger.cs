using System;
using System.Collections.Generic;

namespace HoloCure.NET.Logging
{
    public interface ILogger : IDisposable

    {
    IList<ILogWriter> Writers { get; }

    void Log(string message, ILogLevel level);
    }
}