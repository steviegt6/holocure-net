using HoloCure.EventBus;
using HoloCure.Loader;
using HoloCure.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        /// <summary>
        ///     This game's launch data.
        /// </summary>
        public abstract GameData GameData { get; }

        /// <summary>
        ///     The assembly loader responsible for loading mods for this game instance.
        /// </summary>
        public abstract IAssemblyLoader AssemblyLoader { get; }

        /// <summary>
        ///     The storage provider for this game instance.
        /// </summary>
        public abstract IStorageProvider StorageProvider { get; }

        /// <summary>
        ///     The logger for this game instance.
        /// </summary>
        public abstract ILogger Logger { get; }

        /// <summary>
        ///     The event bus for this game instance.
        /// </summary>
        public abstract MasterEventBus MasterEventBus { get; }

        /// <summary>
        ///     The <see cref="GraphicsDeviceManager"/> instance used by this game.
        /// </summary>
        public virtual GraphicsDeviceManager GraphicsDeviceManager {
            get => (GraphicsDeviceManager) Services.GetService(typeof(GraphicsDeviceManager));

            init {
                if (Services.GetService(typeof(GraphicsDeviceManager)) is not null) Services.RemoveService(typeof(GraphicsDeviceManager));
                Services.AddService(typeof(GraphicsDeviceManager), value);
            }
        }

        public new GraphicsDevice GraphicsDevice => GraphicsDeviceManager.GraphicsDevice;
    }
}