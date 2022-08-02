using System.Reflection;
using System.Runtime.Loader;

namespace HoloCure.Loader
{
    /// <summary>
    ///     Responsible for resolving assemblies depended on by a parent assembly.
    /// </summary>
    public interface IAssemblyResolver
    {
        /// <summary>
        ///     Adds an assembly resolver as a dependency.
        /// </summary>
        /// <param name="resolver">The resolver to register as a dependency.</param>
        void AddDependency(IAssemblyResolver resolver);

        /// <summary>
        ///     Entrypoint for hooking into necessary <see cref="AssemblyLoadContext"/>-related events.
        /// </summary>
        void HookResolution();

        /// <summary>
        ///     Entrypoint for unhooking from necessary <see cref="AssemblyLoadContext"/>-related events.
        /// </summary>
        void UnhookResolution();

        /// <summary>
        ///     Resolves an assembly.
        /// </summary>
        /// <param name="alc">The <see cref="AssemblyLoadContext"/> instance requesting the assembly.</param>
        /// <param name="name">The assembly's <see cref="AssemblyName"/> identity.</param>
        /// <returns>The resolved assembly, null is resolution yielded no results.</returns>
        Assembly? ResolveAssembly(AssemblyLoadContext alc, AssemblyName name);
    }
}