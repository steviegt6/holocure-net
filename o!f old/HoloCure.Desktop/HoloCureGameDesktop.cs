using HoloCure.Game;
using osu.Framework.Platform;

namespace HoloCure.Desktop
{
    public class HoloCureGameDesktop : HoloCureGame
    {
        public override void SetHost(GameHost host)
        {
            base.SetHost(host);

            SDL2DesktopWindow window = (SDL2DesktopWindow)host.Window;

            window.Title = Name;
            // TODO: Make this an option? Original game doesn't use the cursor but keeps it visible.
            window.CursorState |= CursorState.Hidden;
            window.Resizable = false;
        }
    }
}
