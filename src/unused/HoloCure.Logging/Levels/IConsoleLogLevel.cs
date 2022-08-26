using System;

namespace HoloCure.Logging.Levels
{
    /// <summary>
    ///     An <see cref="ILogLevel"/> extension with added context for printing colored text to consoles.
    /// </summary>
    public interface IConsoleLogLevel : ILogLevel
    {
        /// <summary>
        ///     The foreground color of the printed text in a console. <see langkey="null" /> means no color.
        /// </summary>
        ConsoleColor? ForegroundColor { get; }

        /// <summary>
        ///     The background color of the printed text in a console. <see langkey="null" /> means no color.
        /// </summary>
        ConsoleColor? BackgroundColor { get; }
    }
}