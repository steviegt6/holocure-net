using Microsoft.Xna.Framework;

namespace HoloCure.Core
{
    /// <summary>
    ///     The base class of a <see cref="Game"/> that should be launched from a <see cref="IGameLauncher"/>.
    /// </summary>
    public abstract class CoreGame : Game
    {
        /// <summary>
        ///     The launcher used to launch this game.
        /// </summary>
        public abstract IGameLauncher Launcher { get; }
    }
}