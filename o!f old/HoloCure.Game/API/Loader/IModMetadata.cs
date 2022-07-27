using System.Reflection;

namespace HoloCure.Game.API.Loader
{
    /// <summary>
    ///     Meta and runtime information about a mod and its assembly.
    /// </summary>
    public interface IModMetadata
    {
        /// <summary>
        ///     A reliable path to the mod's assembly.
        /// </summary>
        string AssemblyPath { get; set; }

        /// <summary>
        ///     The mod's assembly.
        /// </summary>
        Assembly Assembly { get; set; }

        /// <summary>
        ///     The loaded mod that belongs to this set of metadata.
        /// </summary>
        IMod? Mod { get; set; }

        /// <summary>
        ///     The manifest information belonging to the associated mod.
        /// </summary>
        ModFileManifest? Manifest { get; set; }
    }
}
