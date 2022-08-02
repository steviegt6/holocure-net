using HoloCure.Core;
using HoloCure.Core.Util;
using HoloCure.Loader;
using HoloCure.Logging;
using HoloCure.Logging.Levels;
using HoloCure.NET.Desktop.Loader;
using Microsoft.Extensions.DependencyInjection;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameLauncher : IGameLauncher
    {
        public const string GAME_NAME = "HoloCure";

        public IServiceCollection Dependencies { get; } = new ServiceCollection();

        public DesktopGameLauncher(IStorageProvider storageProvider, ILogger logger, string[] args) {
            Dependencies.AddSingleton<IGameLauncher>(this);
            Dependencies.AddSingleton(new GameData(GAME_NAME, args));
            Dependencies.AddSingleton<IAssemblyLoader>(new AssemblyLoader());
            Dependencies.AddSingleton(storageProvider);
            Dependencies.AddSingleton(logger);
        }

        public CoreGame LaunchGame(string[] args) {
            this.GetAssemblyLoader().LoadMods();
            this.GetLogger().Log($"Launching game from {nameof(DesktopGameLauncher)}...", LogLevels.Debug);
            return new HoloCureGame(this);
        }
    }
}