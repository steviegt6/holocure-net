using HoloCure.NET.API.Loader;
using HoloCure.NET.Desktop.Loader;
using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameLauncher : IGameLauncher
    {
        public IGameBootstrapper Bootstrapper { get; } = new DesktopGameBootstrapper();
        
        public IAssemblyLoader AssemblyLoader { get; } = new DesktopAssemblyLoader();

        public Game? LaunchGame(string[] args) {
            Bootstrapper.Bootstrap(this);
            AssemblyLoader.LoadMods();
            return new HoloCureGame(this, args);
        }
    }
}