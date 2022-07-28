using System;
using System.IO;
using HoloCure.NET.API.Loader;
using HoloCure.NET.Desktop.Loader;
using HoloCure.NET.Desktop.Logging;
using HoloCure.NET.Desktop.Util;
using HoloCure.NET.Launch;
using HoloCure.NET.Logging;
using HoloCure.NET.Util;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameLauncher : IGameLauncher
    {
        public const string GAME_NAME = "HoloCure";

        public IServiceCollection Dependencies { get; } = new ServiceCollection();

        public DesktopGameLauncher() {
            Dependencies.AddSingleton<IGameLauncher>(this);
            Dependencies.AddSingleton(new GameData(GAME_NAME));
            Dependencies.AddSingleton<IGameBootstrapper>(new DesktopGameBootstrapper());
            Dependencies.AddSingleton<IAssemblyLoader>(new DesktopAssemblyLoader());
            IStorageProvider storageProvider = PlatformUtils.MakePlatformDependentStorageProvider(GAME_NAME);
            Dependencies.AddSingleton(storageProvider);
            Dependencies.AddSingleton<ILogger>(
                new DesktopLogger(
                    // Log to the console.
                    new ConsoleLogWriter(),
                    // Log to an archivable log file (one that won't be cleared).
                    new ArchivableFileLogWriter(Path.Combine(storageProvider.GetDirectory(), "game"), ".log", DateTime.Now),
                    // Log to a temporary log file ("latest.log")
                    new TemporaryFileLogWriter(Path.Combine(storageProvider.GetDirectory(), "latest"), ".log")
                )
            );
        }

        public Game LaunchGame(string[] args) {
            ILogger logger = this.GetLogger();
            IGameBootstrapper bootstrapper = this.GetGameBoostrapper();
            logger.Log("Launching game..", LogLevels.Debug);
            bootstrapper.Bootstrap(this);
            return new HoloCureGame(this, args);
        }
    }
}