using HoloCure.Core.API.Loader;
using HoloCure.Core.Launch;
using HoloCure.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace HoloCure.Core.Util
{
    public static class GameLauncherExtensions
    {
        /// <summary>
        ///     Gets a required service from a <see cref="IGameLauncher"/> instance.
        /// </summary>
        /// <param name="launcher">The <see cref="IGameLauncher"/> instance.</param>
        /// <typeparam name="T">The required service type.</typeparam>
        /// <returns></returns>
        public static T GetService<T>(this IGameLauncher launcher)
            where T : notnull {
            using ServiceProvider provider = launcher.Dependencies.BuildServiceProvider();
            return provider.GetRequiredService<T>();
        }

        /// <summary>
        ///     Gets a service from a <see cref="IGameLauncher"/> instance, which may be null.
        /// </summary>
        /// <param name="launcher">The <see cref="IGameLauncher"/> instance.</param>
        /// <typeparam name="T">The service type.</typeparam>
        /// <returns></returns>
        public static T? GetNullableService<T>(this IGameLauncher launcher) {
            using ServiceProvider provider = launcher.Dependencies.BuildServiceProvider();
            return provider.GetService<T>();
        }

        public static IGameLauncher GetLauncher(this IGameLauncher launcher) {
            return launcher.GetService<IGameLauncher>();
        }
        
        public static GameData GetGameData(this IGameLauncher launcher) {
            return launcher.GetService<GameData>();
        }

        public static IAssemblyLoader GetAssemblyLoader(this IGameLauncher launcher) {
            return launcher.GetService<IAssemblyLoader>();
        }
        
        public static IStorageProvider GetStorageProvider(this IGameLauncher launcher) {
            return launcher.GetService<IStorageProvider>();
        }
        
        public static ILogger GetLogger(this IGameLauncher launcher) {
            return launcher.GetService<ILogger>();
        }
    }
}