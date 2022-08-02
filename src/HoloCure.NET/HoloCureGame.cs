using System;
using HoloCure.Core;
using Microsoft.Xna.Framework;

namespace HoloCure.NET
{
    public sealed class HoloCureGame : CoreGame
    {
        public override IGameLauncher Launcher { get; }

        public HoloCureGame(IGameLauncher launcher) {
            Launcher = launcher;
            
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