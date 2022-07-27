using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using HoloCure.Game.API;
using HoloCure.Game.API.Exceptions;
using HoloCure.Game.API.Loader;
using HoloCure.Game.API.Registry;
using Newtonsoft.Json;

namespace HoloCure.Game
{
    public class HoloCureAssemblyLoader : IAssemblyLoader
    {
        public class HoloCureModMetadata : IModMetadata
        {
            public string AssemblyPath { get; set; }

            public Assembly Assembly { get; set; }

            public IMod? Mod { get; set; }

            public ModFileManifest? Manifest { get; set; }

            public HoloCureModMetadata(Assembly assembly, string assemblyPath)
            {
                Assembly = assembly;
                AssemblyPath = assemblyPath;
            }

            public void ResolveManifest()
            {
                // TODO: Either split paths and analyze the actual file name or find a way to search in a fully qualified manner by getting the assembly's root namespace.
                string? manifestFile = Assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(MANIFEST_PATH));

                if (manifestFile is null) throw new MissingManifestFileException("Could not resolve a manifest.json file when loading a mod's assembly.");

                using Stream? fileStream = Assembly.GetManifestResourceStream(manifestFile);

                if (fileStream is null) throw new MissingManifestFileException("Failed to open a stream for the manifest.json file when loading a mod's assembly.");

                using TextReader reader = new StreamReader(fileStream);
                Manifest = JsonConvert.DeserializeObject<ModFileManifest>(reader.ReadToEnd());
            }
        }

        public const string MANIFEST_PATH = "manifest.json";
        public const string ASSEMBLY_PREFIX = "HoloCure.Mod.";

        public ImmutableRegistrar<IModMetadata> ModRegistrar => new(BackingRegistrar);

        protected IRegistrar<IModMetadata> BackingRegistrar = new MutableRegistrar<IModMetadata>();
        protected List<DirectoryInfo> ImmediateProbingPaths = new();
        protected List<DirectoryInfo> DirectoryProbingPaths = new();

        public void AddAssemblyProbingPath(string path) => ImmediateProbingPaths.Add(new DirectoryInfo(path));

        public void AddDirectoryProbingPath(string path) => DirectoryProbingPaths.Add(new DirectoryInfo(path));

        public void ResolveMods()
        {
            // Shallow copy.
            List<DirectoryInfo> directories = ImmediateProbingPaths.ToList();
            directories.AddRange(DirectoryProbingPaths.SelectMany(x => x.EnumerateDirectories()));

            foreach (DirectoryInfo directory in directories)
            {
                FileInfo[] prefixedFiles = directory.GetFiles().Where(x => x.Name.StartsWith(ASSEMBLY_PREFIX)).ToArray();

                // Means there's no mods to load.
                if (prefixedFiles.Length == 0) continue;

                foreach (FileInfo file in prefixedFiles)
                {
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);
                    HoloCureModMetadata metadata = new(assembly, file.FullName);
                }
            }
        }

        public void AddModToLoad(IModMetadata modMetadata)
        {
            if (modMetadata.Manifest is null) throw new NullManifestException($"Manifest for mod located at \"{modMetadata.AssemblyPath}\" is null.");

            BackingRegistrar.Register(new Identifier(modMetadata.Manifest.ModId, "mod"), modMetadata);
        }

        public void SortMods()
        {
            throw new System.NotImplementedException();
        }

        public void PopulateResolvers()
        {
            throw new System.NotImplementedException();
        }
    }
}
