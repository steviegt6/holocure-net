#if COREMODDING
using System.Collections.Generic;
using Felt.Needle.API;
#endif

namespace HoloCure.NET.API
{
    /// <summary>
    ///     A default, simplistic <see cref="IMod"/> implementation. This is what most mods will typically use.
    /// </summary>
    public abstract class DefaultMod : IMod
    {
#if COREMODDING
        public virtual IEnumerable<ICecilPlugin> GetCecilPlugins() {
            yield break;
        }
#endif
    }
}