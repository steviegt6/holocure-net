namespace HoloCure.Logging.Levels
{
    /// <summary>
    ///     Context provided through a log level.
    /// </summary>
    public interface ILogLevel
    {
        /// <summary>
        ///     The human-readable log level name.
        /// </summary>
        string Name { get; }
    }
}