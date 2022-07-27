using System.Reflection;
using HoloCure.NET.API;

namespace HoloCure.NET.Desktop.Loader
{
    public class DesktopModMetadata : IModMetadata
    {
        public string AssemblyPath { get; set; }

        public Assembly? Assembly { get; set; }

        public IMod? Mod { get; set; }

        public ModFileManifest? Manifest { get; set; }

        public DesktopModMetadata(string assemblyPath) {
            AssemblyPath = assemblyPath;
        }
    }
}