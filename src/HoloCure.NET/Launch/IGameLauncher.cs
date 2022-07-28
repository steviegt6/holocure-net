using HoloCure.NET.API.Loader;
using HoloCure.NET.Logging;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Launch
{
    /// <summary>
    ///     Launches a game.
    /// </summary>
    public interface IGameLauncher
    {
        /// <summary>
        ///     The game's name.
        /// </summary>
        string GameName { get; }

        /// <summary>
        ///     The bootstrapper this launcher should bootstrapper launching with.
        /// </summary>
        IGameBootstrapper Bootstrapper { get; }

        /// <summary>
        ///     The assembly loader responsible for loading mods.
        /// </summary>
        IAssemblyLoader AssemblyLoader { get; }

        /// <summary>
        ///     The storage provider for writing permanent data, such as saves and logs.
        /// </summary>
        IStorageProvider StorageProvider { get; }

        /// <summary>
        ///     The logger for this game session.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        ///     Instantiates and launches a game.
        /// </summary>
        /// <param name="args">The game's launch arguments.</param>
        /// <returns>The instantiated game.</returns>
        Game? LaunchGame(string[] args);
    }
}