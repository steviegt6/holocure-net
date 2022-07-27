using System;
using System.Reflection;
using System.Runtime.Loader;
using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop.Launch
{
    public sealed class DesktopGameLauncher : IGameLauncher
    {
        public const string ALC_NAME = "Desktop ALC";
        public IGameBootstrapper Bootstrapper { get; } = new DesktopGameBootstrapper();

        private readonly AssemblyLoadContext LoadContext = new(ALC_NAME);

        public Game LaunchGame(string[] args) {
            AppDomain.CurrentDomain.AssemblyLoad += TransformLoadedAssemblies;
            Bootstrapper.Bootstrap(this);

            Assembly desktopAsm = LoadContext.LoadFromAssemblyPath(typeof(Program).Assembly.Location);
            Type launcher = desktopAsm.GetType("HoloCure.NET.Desktop.Launch.DesktopGameLauncher")!;
            MethodInfo create = launcher.GetMethod("CreateGame", BindingFlags.Static | BindingFlags.NonPublic)!;

            return (Game) create.Invoke(null, new object?[] {this, args})!;
        }

        private static HoloCureGame CreateGame(IGameLauncher launcher, string[] args) {
            return new HoloCureGame(launcher, args);
        }

        private static void TransformLoadedAssemblies(object? sender, AssemblyLoadEventArgs args) {
            Console.WriteLine(args.LoadedAssembly.GetName().Name + " | " + AssemblyLoadContext.GetLoadContext(args.LoadedAssembly)?.Name);
            if (AssemblyLoadContext.GetLoadContext(args.LoadedAssembly)?.Name != ALC_NAME) return;
            if (args.LoadedAssembly.GetName().Name == "FNA") return; // Don't transform FNA

            // TODO: Transform loaded assemblies.
        }
    }
}