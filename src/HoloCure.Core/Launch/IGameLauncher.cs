using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace HoloCure.Core.Launch
{
    /// <summary>
    ///     Launches a game.
    /// </summary>
    public interface IGameLauncher
    {
        /// <summary>
        ///     The central service provider powering game-wide dependency injection.
        /// </summary>
        IServiceCollection Dependencies { get; }
        
        /// <summary>
        ///     Instantiates and launches a game.
        /// </summary>
        /// <param name="args">The game's launch arguments.</param>
        /// <returns>The instantiated game.</returns>
        Game? LaunchGame(string[] args);
    }
}