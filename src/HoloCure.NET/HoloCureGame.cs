using System;
using HoloCure.Core;
using HoloCure.Core.Util;
using HoloCure.EventBus;
using HoloCure.Loader;
using HoloCure.Logging;
using Microsoft.Xna.Framework;

namespace HoloCure.NET
{
    public sealed class HoloCureGame : CoreGame
    {
        public override IGameLauncher Launcher { get; }
        
        public override GameData GameData { get; }
        
        public override IAssemblyLoader AssemblyLoader { get; }
        
        public override IStorageProvider StorageProvider { get; }
        
        public override ILogger Logger { get; }
        
        public override MasterEventBus MasterEventBus { get; }

        public HoloCureGame(IGameLauncher launcher) {
            Launcher = launcher;
            GameData = Launcher.GetGameData();
            AssemblyLoader = Launcher.GetAssemblyLoader();
            StorageProvider = Launcher.GetStorageProvider();
            Logger = Launcher.GetLogger();
            MasterEventBus = Launcher.GetMasterEventBus();
            
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            Window.AllowUserResizing = false;
            Window.Title = "HoloCure";
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            
            base.Draw(gameTime);
        }
    }
}