using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.DependencyModel.Resolution;

// Raw code adapted from: https://www.codeproject.com/Articles/1194332/Resolving-Assemblies-in-NET-Core
// Adapted under The Code Project Open License (CPOL) 1.02: https://www.codeproject.com/info/cpol10.aspx

namespace HoloCure.Game.API.Loader.Resolving
{
    public class DefaultAssemblyResolver : IAssemblyResolver
    {
        public readonly IModMetadata ModMetadata;

        protected readonly List<IAssemblyResolver> Dependencies = new();
        protected readonly DependencyContext DependencyContext;
        protected readonly CompositeCompilationAssemblyResolver Resolver;
        protected readonly AssemblyLoadContext? LoadContext;

        public DefaultAssemblyResolver(IModMetadata modMetadata)
        {
            ModMetadata = modMetadata;

            string asmPath = ModMetadata.AssemblyPath;

            DependencyContext = DependencyContext.Load(ModMetadata.Assembly);
            Resolver = new CompositeCompilationAssemblyResolver(new ICompilationAssemblyResolver[]
            {
                // Resolve from the containing directory.
                new AppBaseCompilationAssemblyResolver(Path.GetDirectoryName(asmPath)),
                new ReferenceAssemblyPathResolver(),
                new PackageCompilationAssemblyResolver()
            });

            LoadContext = AssemblyLoadContext.GetLoadContext(ModMetadata.Assembly);
        }

        public virtual void AddDependency(IAssemblyResolver resolver) => Dependencies.Add(resolver);

        public virtual void EnableResolution()
        {
            if (LoadContext is null) return;

            LoadContext.Resolving += ResolveAssembly;
        }

        public virtual void DisableResolution()
        {
            if (LoadContext is null) return;

            LoadContext.Resolving -= ResolveAssembly;
        }

        public virtual Assembly? ResolveAssembly(AssemblyLoadContext alc, AssemblyName assemblyName)
        {
            bool namesMatch(Library library) => string.Equals(library.Name, assemblyName.Name, StringComparison.OrdinalIgnoreCase);

            // Loop through known dependencies and use their resolvers to resolve the assembly.
            foreach (IAssemblyResolver resolver in Dependencies)
            {
                // TODO: Some check for recursion either here or in AddDependency.
                Assembly? assembly = resolver.ResolveAssembly(alc, assemblyName);

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

            // TODO: Use the supplied ALC instance instead of the one belonging to this type?
            return assemblies.Count != 0 ? LoadContext?.LoadFromAssemblyPath(assemblies[0]) : null;
        }
    }
}
