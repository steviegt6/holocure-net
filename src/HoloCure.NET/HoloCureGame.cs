using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET
{
    public sealed class HoloCureGame : Game, ILaunchable
    {
        public IGameLauncher Launcher { get; }
        
        public string[] Arguments { get; }

        public HoloCureGame(IGameLauncher launcher, string[] arguments) {
            Launcher = launcher;
            Arguments = arguments;
        }
    }
}