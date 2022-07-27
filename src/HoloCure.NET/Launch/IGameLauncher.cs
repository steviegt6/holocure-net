using Microsoft.Xna.Framework;

namespace HoloCure.NET.Launch
{
    /// <summary>
    ///     Launches a game.
    /// </summary>
    public interface IGameLauncher
    {
        /// <summary>
        ///     The bootstrapper this launcher should bootstrapper launching with.
        /// </summary>
        IGameBootstrapper Bootstrapper { get; }

        /// <summary>
        ///     Instantiates and launches a game.
        /// </summary>
        /// <param name="args">The game's launch arguments.</param>
        /// <returns>The instantiated game.</returns>
        Game LaunchGame(string[] args);
    }
}