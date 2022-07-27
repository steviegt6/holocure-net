using osu.Framework.Platform;
using osu.Framework;
using HoloCure.Game;

namespace HoloCure.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using GameHost host = Host.GetSuitableDesktopHost(HoloCureGameBase.GAME_NAME);
            using osu.Framework.Game game = new HoloCureGameDesktop();
            host.Run(game);
        }
    }
}
