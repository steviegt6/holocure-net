using System;
using HoloCure.Logging.Levels;

namespace HoloCure.Logging
{
    /// <summary>
    ///     An object capable of
    /// </summary>
    public interface ILogWriter : IDisposable
    {
        /// <summary>
        ///     Writes the given message with context provided through a log level.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="level">The level.</param>
        void Log(string message, ILogLevel level);
    }
}