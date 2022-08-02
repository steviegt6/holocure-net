using System.Reflection;
using HoloCure.Loader;

namespace HoloCure.NET.Desktop.Loader
{
    public class ModMetadata : IModMetadata
    {
        public string AssemblyPath { get; set; }

        public Assembly? Assembly { get; set; }

        public IMod? Mod { get; set; }

        public ModFileManifest? Manifest { get; set; }

        public ModMetadata(string assemblyPath) {
            AssemblyPath = assemblyPath;
        }
    }
}