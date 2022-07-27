using HoloCure.Game.Screens;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace HoloCure.Game
{
    public class HoloCureGame : HoloCureGameBase
    {
        protected ScreenStack BackgroundStack = null!;
        protected ScreenStack ForegroundStack = null!;

        [BackgroundDependencyLoader]
        private void load()
        {
            Children = new[]
            {
                BackgroundStack = new ScreenStack { RelativeSizeAxes = Axes.Both },
                ForegroundStack = new ScreenStack { RelativeSizeAxes = Axes.Both }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            ForegroundStack.ScreenPushed += foregroundScreenPushed;

            ForegroundStack.Push(new MainScreen());
        }

        private void foregroundScreenPushed(IScreen lastScreen, IScreen newScreen)
        {
            if (newScreen is not IBackgroundProvider provider) return;

            IScreen currentBackground = BackgroundStack.CurrentScreen;
            IScreen newBackground = provider.GetBackgroundScreen(currentBackground);

            if (currentBackground != newBackground) BackgroundStack.Push(newBackground);
        }
    }
}
