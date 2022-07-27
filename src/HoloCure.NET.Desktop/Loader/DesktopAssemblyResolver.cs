using System.Reflection;
using System.Runtime.Loader;
using HoloCure.NET.API;
using HoloCure.NET.API.Loader;

// Raw code adapted from: https://www.codeproject.com/Articles/1194332/Resolving-Assemblies-in-NET-Core
// Adapted under The Code Project Open License (CPOL) 1.02: https://www.codeproject.com/info/cpol10.aspx

namespace HoloCure.NET.Desktop.Loader
{
    public class DesktopAssemblyResolver : IAssemblyResolver
    {
        public readonly IModMetadata ModMetadata;

        public DesktopAssemblyResolver(IModMetadata modMetadata) {
            ModMetadata = modMetadata;
        }
        
        public void AddDependency(IAssemblyResolver resolver) {
            throw new System.NotImplementedException();
        }

        public void HookResolution() {
            throw new System.NotImplementedException();
        }

        public void UnhookResolution() {
            throw new System.NotImplementedException();
        }

        public Assembly? ResolveAssembly(AssemblyLoadContext alc, AssemblyName name) {
            throw new System.NotImplementedException();
        }
    }
}