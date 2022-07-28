using HoloCure.NET.API.Loader;
using HoloCure.NET.Launch;
using HoloCure.NET.Logging;
using HoloCure.NET.Util;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameBootstrapper : IGameBootstrapper
    {
        public void Bootstrap(IGameLauncher launcher) {
            ILogger logger = launcher.GetLogger();
            IAssemblyLoader assemblyLoader = launcher.GetAssemblyLoader();
            logger.Log("Bootstrapping game launch...", LogLevels.Debug);
            logger.Log("Initializing FNA native dependency resolution...", LogLevels.Debug);
            FnaBootstrap.Initialize_FNA();
            logger.Log("Loading mods...", LogLevels.Debug);
            assemblyLoader.LoadMods();
        }
    }
}