using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameLauncher : IGameLauncher
    {
        public IGameBootstrapper Bootstrapper { get; } = new DesktopGameBootstrapper();

        public Game LaunchGame(string[] args) {
            Bootstrapper.Bootstrap(this);
            return new HoloCureGame(this, args);
        }
    }
}