using System;
using System.IO;
using HoloCure.NET.API.Loader;
using HoloCure.NET.Desktop.Loader;
using HoloCure.NET.Desktop.Logging;
using HoloCure.NET.Desktop.Util;
using HoloCure.NET.Launch;
using HoloCure.NET.Logging;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameLauncher : IGameLauncher
    {
        public const string GAME_NAME = "HoloCure";

        public string GameName => GAME_NAME;

        public IGameBootstrapper Bootstrapper { get; } = new DesktopGameBootstrapper();

        public IAssemblyLoader AssemblyLoader { get; } = new DesktopAssemblyLoader();

        public IStorageProvider StorageProvider { get; } = PlatformUtils.MakePlatformDependentStorageProvider(GAME_NAME);

        public ILogger Logger { get; }

        public DesktopGameLauncher() {
            Logger = new DesktopLogger(
                // Log to the console.
                new ConsoleLogWriter(),
                // Log to an archivable log file (one that won't be cleared).
                new ArchivableFileLogWriter(Path.Combine(StorageProvider.GetDirectory(), "game"), ".log", DateTime.Now),
                // Log to a temporary log file ("latest.log")
                new TemporaryFileLogWriter(Path.Combine(StorageProvider.GetDirectory(), "latest"), ".log")
            );
        }

        public Game LaunchGame(string[] args) {
            Logger.Log("Launching game..", LogLevels.Debug);
            
            Bootstrapper.Bootstrap(this);

            return new HoloCureGame(this, args);
        }
    }
}