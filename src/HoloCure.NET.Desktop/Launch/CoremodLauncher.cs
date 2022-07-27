using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop.Launch
{
    public class CoremodLauncher : IGameLauncher
    {
        public const string ALC_NAME = "Desktop ALC";
        public IGameBootstrapper Bootstrapper { get; } = new CoremodBootstrapper();

        private readonly AssemblyLoadContext LoadContext = new(ALC_NAME);

        public Game? LaunchGame(string[] args) {
            AppDomain.CurrentDomain.AssemblyLoad += TransformLoadedAssemblies;
            Bootstrapper.Bootstrap(this);

            Assembly desktopAsm = ReloadAssemblies();
            Type launcher = desktopAsm.GetType("HoloCure.NET.Desktop.Program")!;
            MethodInfo create = launcher.GetMethod("Main", BindingFlags.Static | BindingFlags.Public)!;

            create.Invoke(null, new object?[] {args.Concat(new[] {"--skip-coremods"}).ToArray()});

            return null;
        }

        private Assembly ReloadAssemblies() {
            Assembly desktopAsm = null!;
            List<Assembly> asmsToReload = new();

            foreach (Assembly assembly in AssemblyLoadContext.Default.Assemblies) {
                string name = assembly.GetName().Name ?? "";

                if (name == "mscorlib") continue; // Skip core library.
                if (name == "System" || name.StartsWith("System.")) continue; // Skip System assemblies.
                // if (name == "FNA") continue; // Skip FNA.

                asmsToReload.Add(assembly);
            }

            foreach (Assembly assembly in asmsToReload) {
                string name = assembly.GetName().Name ?? "";
                Assembly asm = LoadContext.LoadFromAssemblyPath(assembly.Location);

                if (name == "HoloCure.NET.Desktop") desktopAsm = asm;
            }

            return desktopAsm;
        }

        private static void TransformLoadedAssemblies(object? sender, AssemblyLoadEventArgs args) {
            Console.WriteLine(args.LoadedAssembly.GetName().Name + " | " + AssemblyLoadContext.GetLoadContext(args.LoadedAssembly)?.Name);
            if (AssemblyLoadContext.GetLoadContext(args.LoadedAssembly)?.Name != ALC_NAME) return;
            if (args.LoadedAssembly.GetName().Name == "FNA") return; // Don't transform FNA

            // TODO: Transform loaded assemblies.
        }
    }
}