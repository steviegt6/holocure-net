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