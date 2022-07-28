﻿using System;
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

        public DesktopGameLauncher(IStorageProvider storageProvider, ILogger logger, string[] args) {
            Dependencies.AddSingleton<IGameLauncher>(this);
            Dependencies.AddSingleton(new GameData(GAME_NAME, args));
            Dependencies.AddSingleton<IAssemblyLoader>(new DesktopAssemblyLoader());
            Dependencies.AddSingleton(storageProvider);
            Dependencies.AddSingleton(logger);
        }

        public Game LaunchGame(string[] args) {
            this.GetAssemblyLoader().LoadMods();
            this.GetLogger().Log("Launching game...", LogLevels.Debug);
            return new HoloCureGame(this, args);
        }
    }
}