using HoloCure.NET.Desktop.Launch;
using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop
{
    public static class Program
    {
        public static void Main(string[] args) {
            IGameLauncher launcher = CreateLauncher();
            using Game game = launcher.LaunchGame(args);
            game.Run();
        }

        public static IGameLauncher CreateLauncher() {
            return new DesktopGameLauncher();
        }
    }
}