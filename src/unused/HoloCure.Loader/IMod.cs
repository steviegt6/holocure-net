using HoloCure.EventBus;
using HoloCure.Logging;
using Microsoft.Extensions.DependencyInjection;
#if COREMODDING
using System.Collections.Generic;
using Felt.Needle.API;
#endif

namespace HoloCure.Loader
{
    /// <summary>
    ///     Represents a mod, which has ownership over its associated content and assembly.
    /// </summary>
    public interface IMod
    {
        /// <summary>
        ///     This mod's <see cref="IEventBus"/> instance.
        /// </summary>
        IEventBus EventBus { get; }

        /// <summary>
        ///     The logger.
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        ///     External dependencies granted to this mod.
        /// </summary>
        IServiceCollection Dependencies { get; set; }

        /// <summary>
        ///     Called when this mod is first loaded.
        /// </summary>
        void Initialize();

#if COREMODDING
        /// <summary>
        ///     Retrieves an enumerable collection of cecil plugins for assembly transformation. <br />
        ///     This is only used during the coremodding loading stage and if your manifest file states that coremodding for your mod is enabled.
        /// </summary>
        /// <returns>An enumerable collection of cecil plugins.</returns>
        IEnumerable<ICecilPlugin> GetCecilPlugins();
#endif
    }
}