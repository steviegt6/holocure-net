using System.Reflection;

namespace HoloCure.Core.API
{
    /// <summary>
    ///     Runtime information about a <see cref="IMod"/>, created and partially populated prior to the instantiation of a <see cref="IMod"/>.
    /// </summary>
    public interface IModMetadata
    {
        /// <summary>
        ///     The assembly path of the mod.
        /// </summary>
        string AssemblyPath { get; set; }

        /// <summary>
        ///     The loaded mod assembly.
        /// </summary>
        Assembly? Assembly { get; set; }

        /// <summary>
        ///     The loaded mod instance. Null before the mod is loaded and when the mod fails to load.
        /// </summary>
        IMod? Mod { get; set; }

        /// <summary>
        ///     The <see cref="Mod"/>'s manifest file.
        /// </summary>
        ModFileManifest? Manifest { get; set; }
    }
}