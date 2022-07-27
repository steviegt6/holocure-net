using osu.Framework;
using osu.Framework.Platform;

namespace HoloCure.Game.Tests
{
    public static class Program
    {
        public static void Main()
        {
            using GameHost host = Host.GetSuitableDesktopHost(HoloCureGameBase.GAME_NAME);
            using osu.Framework.Game game = new HoloCureTestBrowser();
            host.Run(game);
        }
    }
}
