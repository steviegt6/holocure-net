using System.Reflection;
using Felt.Needle.Visitors;
using Microsoft.Xna.Framework;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;

namespace HoloCure.Mod.Vanilla.Test
{
    public class VisitorTest : MethodDefinitionVisitor
    {
        public override void Visit(MethodDefinition memberDefinition) {
            if (memberDefinition.DeclaringType.FullName != "HoloCure.NET.HoloCureGame" || memberDefinition.Name != "Draw") return;

            ILCursor c = new(new ILContext(memberDefinition));
            c.TryGotoNext(x => x.MatchCall<Color>("get_Black"));
            c.Remove();
            c.Emit(OpCodes.Call, typeof(Color).GetProperty("Red", BindingFlags.Static | BindingFlags.Public)!.GetGetMethod());
        }
    }
}