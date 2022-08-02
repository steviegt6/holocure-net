using System.IO;

namespace HoloCure.Core
{
    /// <summary>
    ///     Provides a path to a base storage directory.
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        ///     Provides the base storage directory.
        /// </summary>
        /// <returns>The fully qualified storage directory path.</returns>
        string GetDirectory();

        /// <summary>
        ///     Provides the base storage directory.
        /// </summary>
        /// <returns><see cref="GetDirectory"/> as a <see cref="DirectoryInfo"/> object.</returns>
        DirectoryInfo GetDirectoryInfo();
    }
}