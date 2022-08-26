#if false
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using HoloCure.Loader;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

// Raw code adapted from: https://www.codeproject.com/Articles/1194332/Resolving-Assemblies-in-NET-Core
// Adapted under The Code Project Open License (CPOL) 1.02: https://www.codeproject.com/info/cpol10.aspx

namespace HoloCure.NET.Desktop.Loader
{
    public class AssemblyResolver : IAssemblyResolver
    {
        public readonly IModMetadata ModMetadata;

        protected readonly List<IAssemblyResolver> Dependencies = new();
        protected readonly DependencyContext DependencyContext;
        protected readonly CompositeCompilationAssemblyResolver Resolver;
        protected readonly AssemblyLoadContext LoadContext;
        
        public AssemblyResolver(IModMetadata modMetadata, AssemblyLoadContext alc) {
            ModMetadata = modMetadata;

            string asmPath = ModMetadata.AssemblyPath;

            ModMetadata.Assembly = alc.LoadFromAssemblyPath(asmPath);
            DependencyContext = DependencyContext.Load(ModMetadata.Assembly);
            Resolver = new CompositeCompilationAssemblyResolver(new ICompilationAssemblyResolver[]
            {
                new AppBaseCompilationAssemblyResolver(Path.GetDirectoryName(asmPath)),
                new ReferenceAssemblyPathResolver(),
                new PackageCompilationAssemblyResolver()
            });
            LoadContext = alc;
        }
        
        public void AddDependency(IAssemblyResolver resolver) {
            Dependencies.Add(resolver);
        }

        public void HookResolution() {
            LoadContext.Resolving += ResolveAssembly;
        }

        public void UnhookResolution() {
            LoadContext.Resolving -= ResolveAssembly;
        }

        public Assembly? ResolveAssembly(AssemblyLoadContext alc, AssemblyName asmName) {
            bool namesMatch(Library library) => string.Equals(library.Name, asmName.Name, StringComparison.OrdinalIgnoreCase);

            // Loop through known dependencies and use their resolvers to resolve the assembly.
            foreach (IAssemblyResolver resolver in Dependencies)
            {
                // TODO: Some check for recursion either here or in AddDependency.
                Assembly? assembly = resolver.ResolveAssembly(alc, asmName);

                if (assembly is not null) return assembly;
            }

            RuntimeLibrary? library = DependencyContext.RuntimeLibraries.FirstOrDefault(namesMatch);

            if (library is null) return null;

            List<string> assemblies = new();
            CompilationLibrary wrapper = new(
                library.Type,
                library.Name,
                library.Version,
                library.Hash,
                library.RuntimeAssemblyGroups.SelectMany(x => x.AssetPaths),
                library.Dependencies,
                library.Serviceable
            );

            Resolver.TryResolveAssemblyPaths(wrapper, assemblies);

            return assemblies.Count != 0 ? LoadContext.LoadFromAssemblyPath(assemblies[0]) : null;
        }
    }
}
#endif