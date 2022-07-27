using System.Collections.Generic;
using Felt.Needle.API;
using Felt.Needle.Visitors;
using HoloCure.NET.API;

namespace HoloCure.Mod.Vanilla
{
    public class VanillaMod : IMod
    {
        public IEnumerable<ICecilPlugin> GetCecilPlugins() {
            VisitorPlugin visitorPlugin = new VisitorPlugin();
            //visitorPlugin.AddVisitor();
            yield return visitorPlugin;
        }
    }
}