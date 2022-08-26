using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace HoloCure.Core
{
    /// <summary>
    ///     Launches a game.
    /// </summary>
    public interface IGameLauncher
    {
        /// <summary>
        ///     This launcher's dependency provider.
        /// </summary>
        IServiceCollection Dependencies { get; }
        
        /// <summary>
        ///     Instantiates and launches a game.
        /// </summary>
        /// <param name="args">The game's launch arguments.</param>
        /// <returns>The instantiated game.</returns>
        CoreGame? LaunchGame(string[] args);
    }
}