using System;
using System.Collections.Generic;

namespace HoloCure.Core.Logging
{
    public interface ILogger : IDisposable

    {
    IList<ILogWriter> Writers { get; }

    void Log(string message, ILogLevel level);
    }
}