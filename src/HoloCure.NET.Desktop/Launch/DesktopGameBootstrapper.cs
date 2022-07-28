using HoloCure.NET.Launch;
using HoloCure.NET.Logging;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameBootstrapper : IGameBootstrapper
    {
        public void Bootstrap(IGameLauncher launcher) {
            launcher.Logger.Log("Bootstrapping game launch...", LogLevels.Debug);
            launcher.Logger.Log("Initializing FNA native dependency resolution...", LogLevels.Debug);
            FnaBootstrap.Initialize_FNA();
            launcher.Logger.Log("Loading mods...", LogLevels.Debug);
            launcher.AssemblyLoader.LoadMods();
        }
    }
}