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
    ///     A default, simplistic <see cref="IMod"/> implementation. This is what most mods will typically use.
    /// </summary>
    public abstract class DefaultMod : IMod
    {
        public virtual IEventBus EventBus { get; } = new SimpleEventBus();

        public virtual ILogger Logger { get; } = null!;

        public virtual IServiceCollection Dependencies { get; set; } = null!;

        public virtual void Initialize() {
        }

#if COREMODDING
        public virtual IEnumerable<ICecilPlugin> GetCecilPlugins() {
            yield break;
        }
#endif
    }
}