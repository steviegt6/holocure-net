using System.Collections.Generic;
using System.IO;

namespace HoloCure.NET.API.Loader
{
    /// <summary>
    ///     A prober for resolving valid assembly files.
    /// </summary>
    public interface IAssemblyProber
    {
        /// <summary>
        ///     Probes for valid assembly files.
        /// </summary>
        /// <returns>An enumerable collection of assembly files.</returns>
        IEnumerable<FileInfo> Probe();
    }
}