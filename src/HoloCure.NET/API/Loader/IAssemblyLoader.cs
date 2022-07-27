using System.Collections.Generic;
using HoloCure.NET.API.Registry;

namespace HoloCure.NET.API.Loader
{
    /// <summary>
    ///     Handles resolving and loading mod assemblies.
    /// </summary>
    public interface IAssemblyLoader
    {
        /// <summary>
        ///     An immutable registrar for mods.
        /// </summary>
        ImmutableRegistrar<IModMetadata> ModRegistrar { get; }

        /// <summary>
        ///     Adds an assembly prober to this loader.
        /// </summary>
        /// <param name="prober">The prober to add.</param>
        void AddProber(IAssemblyProber prober);

        IEnumerable<IModMetadata> ResolveMods();

        IEnumerable<IModMetadata> OrganizeMods(IAssemblyOrganizer organizer);

        void LoadMods();
    }
}