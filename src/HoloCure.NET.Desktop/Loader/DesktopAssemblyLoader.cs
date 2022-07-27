using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using HoloCure.NET.API;
using HoloCure.NET.API.Loader;
using HoloCure.NET.API.Registry;
using HoloCure.NET.Desktop.Util;

namespace HoloCure.NET.Desktop.Loader
{
    public class DesktopAssemblyLoader : IAssemblyLoader
    {
        public const string PREFIX = "HoloCure.Mod.";
        public const string MANIFEST_NAME = "manifest.json";

        protected readonly IRegistrar<IModMetadata> Registrar = new MutableRegistrar<IModMetadata>();

        public ImmutableRegistrar<IModMetadata> ModRegistrar => new(Registrar);

        protected readonly List<IAssemblyProber> Probers = new();
        protected readonly Dictionary<IModMetadata, IAssemblyResolver> Resolvers = new();

        public DesktopAssemblyLoader() {
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

                IModMetadata metadata = new DesktopModMetadata(file.FullName);
                Resolvers[metadata] = new DesktopAssemblyResolver(metadata, AssemblyLoadContext.GetLoadContext(typeof(Program).Assembly)!);
                yield return metadata;
            }
        }

        public IEnumerable<IModMetadata> OrganizeMods(IAssemblyOrganizer organizer) {
            // TODO
            return ResolveMods();
        }

        public void LoadMods() {
            List<IModMetadata> metadata = OrganizeMods(new DesktopAssemblyOrganizer()).ToList();

            foreach (IModMetadata modMetadata in metadata) {
                Resolvers[modMetadata].HookResolution();
                modMetadata.LoadManifestFile();
                modMetadata.InstantiateMod();
                Registrar.Register(new Identifier("engine", modMetadata.Manifest!.ModId), modMetadata);
            }
        }
    }
}