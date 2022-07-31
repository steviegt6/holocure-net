using Microsoft.Xna.Framework;

namespace HoloCure.Core.Launch
{
    /// <summary>
    ///     Represents a <see cref="Game"/> that should be launched from a <see cref="IGameLauncher"/>.
    /// </summary>
    public interface ILaunchableGame
    {
        /// <summary>
        ///     The launcher used to launch this game.
        /// </summary>
        IGameLauncher Launcher { get; }
    }
}