#if false
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using HoloCure.Core;
using HoloCure.Loader;
using HoloCure.NET.Desktop.Loader.Probers;
using HoloCure.NET.Desktop.Util;
using HoloCure.NET.Registries;
using HoloCure.Registry;

namespace HoloCure.NET.Desktop.Loader
{
    public class AssemblyLoader : IAssemblyLoader
    {
        public const string PREFIX = "HoloCure.Mod.";
        public const string MANIFEST_NAME = "manifest.json";

        protected readonly IGameLauncher Launcher;
        protected readonly List<IAssemblyProber> Probers = new();
        protected readonly Dictionary<IModMetadata, IAssemblyResolver> Resolvers = new();

        public AssemblyLoader(IGameLauncher launcher) {
            Launcher = launcher;

            AddProber(new TopLevelDirectoryProber(Environment.CurrentDirectory));

            string? loc = Path.GetDirectoryName(typeof(Program).Assembly.Location);
            if (loc is not null) AddProber(new TopLevelDirectoryProber(loc));
        }

        public void AddProber(IAssemblyProber prober) {
            Probers.Add(prober);
        }

        public IEnumerable<IModMetadata> ResolveMods() {
            IEnumerable<FileInfo> files = Probers.SelectMany(x => x.Probe()).DistinctBy(x => x.FullName);

            foreach (FileInfo file in files) {
                if (file.Extension != ".dll") continue;
                if (!file.Name.StartsWith(PREFIX)) continue;

                IModMetadata metadata = new ModMetadata(file.FullName);
                Resolvers[metadata] = new AssemblyResolver(metadata, AssemblyLoadContext.GetLoadContext(typeof(Program).Assembly)!);
                yield return metadata;
            }
        }

        public IEnumerable<IModMetadata> OrganizeMods(IAssemblyOrganizer organizer) {
            // TODO
            return ResolveMods();
        }

        public void LoadMods() {
            List<IModMetadata> metadata = OrganizeMods(new AssemblyOrganizer()).ToList();

            foreach (IModMetadata modMetadata in metadata) {
                Resolvers[modMetadata].HookResolution();
                modMetadata.LoadManifestFile();
                modMetadata.InstantiateMod(Launcher);
                GlobalRegistrar.ModRegistrar.Register(new Identifier(modMetadata.Manifest!.ModId, "mod"), modMetadata);
            }
        }
    }
}
#endif