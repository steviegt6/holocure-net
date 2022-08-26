#if COREMODDING
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Felt.Needle;
using Felt.Needle.API;
using HoloCure.NET.API.Loader;
using HoloCure.NET.Desktop.Loader;
using HoloCure.NET.Desktop.Util;
using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;
using Mono.Cecil;

namespace HoloCure.NET.Desktop.Launch
{
    public class CoremodLauncher : IGameLauncher
    {
        public const string ALC_NAME = "Desktop ALC";

        public string GameName => DesktopGameLauncher.GAME_NAME;
        public IGameBootstrapper Bootstrapper { get; } = new CoremodBootstrapper();

        public IAssemblyLoader AssemblyLoader { get; } = new DesktopAssemblyLoader();

        public IStorageProvider StorageProvider { get; } = PlatformUtils.MakePlatformDependentStorageProvider(DesktopGameLauncher.GAME_NAME);

        private readonly AssemblyLoadContext LoadContext = new(ALC_NAME);

        private readonly IModuleHandler ModuleHandler = new StandardModuleHandler(
            new StandardModuleResolver(),
            new StandardModuleWriter(),
            new StandardModuleTransformer()
        );

        public Game? LaunchGame(string[] args) {
            // AppDomain.CurrentDomain.AssemblyLoad += TransformAssembly;
            Bootstrapper.Bootstrap(this);
            AssemblyLoader.LoadMods();

            Assembly desktopAsm = ReloadAssemblies();
            Type program = desktopAsm.GetType("HoloCure.NET.Desktop.Program")!;
            MethodInfo main = program.GetMethod("Main", BindingFlags.Static | BindingFlags.Public)!;

            main.Invoke(null, new object?[] {args.Concat(new[] {"--skip-coremods"}).ToArray()});

            return null;
        }

        private Assembly ReloadAssemblies() {
            Assembly desktopAsm = null!;
            List<Assembly> asmsToReload = new();

            foreach (Assembly assembly in AssemblyLoadContext.Default.Assemblies) {
                string name = assembly.GetName().Name ?? "";

                if (name == "mscorlib") continue; // Skip core library.
                if (name == "System" || name.StartsWith("System.")) continue; // Skip System assemblies.
                if (assembly.IsDynamic) continue; // Skip dynamic assemblies.
                // if (name == "FNA") continue; // Skip FNA.

                asmsToReload.Add(assembly);
            }

            foreach (Assembly assembly in asmsToReload) {
                string name = assembly.GetName().Name ?? "";
                using Stream transformedStream = TransformAssembly(assembly);
                Assembly asm = LoadContext.LoadFromStream(transformedStream);

                if (name == "HoloCure.NET.Desktop") desktopAsm = asm;
            }

            return desktopAsm;
        }

        private Stream TransformAssembly(Assembly asm) {
            Console.WriteLine(asm.GetName().Name + " | " + AssemblyLoadContext.GetLoadContext(asm)?.Name);
            // if (AssemblyLoadContext.GetLoadContext(asm)?.Name != ALC_NAME) return;

            // TODO: Null safety
            ModuleDefinition module = ModuleHandler.ModuleResolver.ResolveFromPath(asm.Location)!;
            IEnumerable<ICecilPlugin> plugins = AssemblyLoader.ModRegistrar.RegisteredContent.Values
                                                              .Select(x => x.Mod)
                                                              .SelectMany(x => x!.GetCecilPlugins());
            
            foreach (ICecilPlugin plugin in plugins) {
                plugin.TransformModule(module);
            }

            MemoryStream stream = new();
            ModuleHandler.ModuleWriter.Write(module, stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
#endif