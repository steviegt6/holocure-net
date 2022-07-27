using System.Runtime.Loader;
using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameLauncher : IGameLauncher
    {
        public const string ALC_NAME = "Desktop ALC";
        public IGameBootstrapper Bootstrapper { get; } = new DesktopGameBootstrapper();

        private readonly AssemblyLoadContext LoadContext = new(ALC_NAME);

        public Game? LaunchGame(string[] args) {
            Bootstrapper.Bootstrap(this);

            return new HoloCureGame(this, args);
        }
    }
}