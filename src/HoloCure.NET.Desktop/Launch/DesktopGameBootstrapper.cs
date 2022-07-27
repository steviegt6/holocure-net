using HoloCure.NET.Launch;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameBootstrapper : IGameBootstrapper
    {
        public void Bootstrap(IGameLauncher launcher) {
            FnaBootstrap.Initialize_FNA();
        }
    }
}