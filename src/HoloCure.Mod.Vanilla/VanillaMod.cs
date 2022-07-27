using System.Collections.Generic;
using Felt.Needle.API;
using Felt.Needle.Visitors;
using HoloCure.Mod.Vanilla.Test;
using HoloCure.NET.API;

namespace HoloCure.Mod.Vanilla
{
    public class VanillaMod : IMod
    {
        public IEnumerable<ICecilPlugin> GetCecilPlugins() {
            VisitorPlugin visitorPlugin = new();
            visitorPlugin.AddVisitor(new VisitorTest());
            yield return visitorPlugin;
        }
    }
}