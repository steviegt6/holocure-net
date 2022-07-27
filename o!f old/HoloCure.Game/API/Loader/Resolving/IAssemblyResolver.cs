using System.Reflection;
using System.Runtime.Loader;

namespace HoloCure.Game.API.Loader.Resolving
{
    /// <summary>
    ///     Represents an object capable of handling assembly resolution for a given assembly.
    /// </summary>
    public interface IAssemblyResolver
    {
        /// <summary>
        ///     Registers another resolver as a dependency.
        /// </summary>
        /// <param name="resolver">The resolver to register as a dependency.</param>
        void AddDependency(IAssemblyResolver resolver);

        /// <summary>
        ///     Allows this resolver to subscribe to any necessary events, etc., informing the resolver to begin resolving.
        /// </summary>
        void EnableResolution();

        /// <summary>
        ///     Allows this resolver to unsubscribe from any necessary events, etc., informing the resolver to stop resolving.
        /// </summary>
        void DisableResolution();

        /// <summary>
        ///     Resolves an assembly using this resolver.
        /// </summary>
        /// <param name="alc">The <see cref="AssemblyLoadContext"/> to load from.</param>
        /// <param name="assemblyName">The assembly to load, identified using an <see cref="AssemblyName"/>.</param>
        /// <returns>The resolved assembly, null if unresolved.</returns>
        Assembly? ResolveAssembly(AssemblyLoadContext alc, AssemblyName assemblyName);
    }
}
