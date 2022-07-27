using HoloCure.Game.API.Registry;

namespace HoloCure.Game.API.Loader
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
        ///     Registers a probing path to search for mod assemblies in.
        /// </summary>
        /// <param name="path"></param>
        void AddAssemblyProbingPath(string path);

        /// <summary>
        ///     Registers a probing path to search for directories containing mod assemblies in.
        /// </summary>
        /// <param name="path"></param>
        void AddDirectoryProbingPath(string path);

        /// <summary>
        ///     Resolves mods that should be loaded.
        /// </summary>
        void ResolveMods();

        /// <summary>
        ///     Adds a mod for loading.
        /// </summary>
        /// <param name="modMetadata">The mod metadata to queue.</param>
        void AddModToLoad(IModMetadata modMetadata);

        /// <summary>
        ///     Sorts mods based on dependency information.
        /// </summary>
        void SortMods();

        /// <summary>
        ///     Populates the dependencies of <see cref="IAssemblyLoader"/> instances.
        /// </summary>
        void PopulateResolvers();
    }
}
