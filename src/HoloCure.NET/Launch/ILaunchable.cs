namespace HoloCure.NET.Launch
{
    /// <summary>
    ///     Represents a <see cref="Game"/> that should be launched from a <see cref="IGameLauncher"/>.
    /// </summary>
    public interface ILaunchable
    {
        /// <summary>
        ///     The launcher used to launch this game.
        /// </summary>
        IGameLauncher Launcher { get; }

        /// <summary>
        ///     The launch arguments passed to this game by the launcher.
        /// </summary>
        string[] Arguments { get; }
    }
}