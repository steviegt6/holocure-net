﻿using System;
using HoloCure.Core.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET
{
    public sealed class HoloCureGame : Game, ILaunchableGame
    {
        public IGameLauncher Launcher { get; }

        public string[] Arguments { get; }

        public HoloCureGame(IGameLauncher launcher, string[] arguments) {
            Launcher = launcher;
            Arguments = arguments;
            
            Services.AddService(typeof(GraphicsDeviceManager), new GraphicsDeviceManager(this));

            Window.AllowUserResizing = false;
            Window.Title = "HoloCure";
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            
            base.Draw(gameTime);
        }
    }
}