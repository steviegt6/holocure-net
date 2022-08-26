using System.Collections.Generic;
using HoloCure.Logging.Levels;

namespace HoloCure.Logging
{
    /// <summary>
    ///     A logger which may write messages to several <see cref="ILogWriter"/>s. <br />
    ///     Functionally a collection of <see cref="ILogWriter"/>s, as <see cref="ILogger"/> also implements <see cref="ILogWriter"/>.
    /// </summary>
    public interface ILogger : ILogWriter
    {
        /// <summary>
        ///     A collection of writers that the logger writes to.
        /// </summary>
        IList<ILogWriter> Writers { get; }
        
        /// <summary>
        ///     Writes a given message (without modifications) with context provided through a log level.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="level">The level.</param>
        void LogLiteral(string message, ILogLevel level);

        /// <summary>
        ///     Writes a given message with context provided through a log level a single time.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="level">The level.</param>
        void LogOnce(string message, ILogLevel level);

        /// <summary>
        ///     Writes a given message (without modifications) with context provided through a log level a single time.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// <param name="level">The level.</param>
        void LogLiteralOnce(string message, ILogLevel level);
    }
}