using System.Collections.Generic;

namespace HoloCure.Game.API.Registry
{
    /// <summary>
    ///     Registrar for content.
    /// </summary>
    /// <typeparam name="T">The type of content that may be registered.</typeparam>
    public interface IRegistrar<T>
        where T : notnull
    {
        /// <summary>
        ///     A dictionary mapping identifiers to their associated registered content.
        /// </summary>
        /// <remarks>
        ///     This dictionary may be immutable and should not be used for direct lookups under normal circumstances.
        ///     When retrieving and adding content, you should interface with <see cref="Register"/> and <see cref="Get"/>.
        /// </remarks>
        IDictionary<Identifier, T> RegisteredContent { get; }

        /// <summary>
        ///     Reverse lookup mapping registered content to an associated identifier.
        /// </summary>
        /// <remarks>
        ///     The same caveats that apply to <see cref="RegisteredContent"/> apply to <see cref="ReverseLookup"/>.
        ///     You should interface with <see cref="GetId"/> for retrieval, and should never try to manually add to this map.
        /// </remarks>
        IDictionary<T, Identifier> ReverseLookup { get; }

        /// <summary>
        ///     Register an instance, given an identifier.
        /// </summary>
        /// <param name="id">The associated identifier.</param>
        /// <param name="entry">The content instance.</param>
        /// <returns>The registered content instance.</returns>
        T Register(Identifier id, T entry);

        /// <summary>
        ///     Retrieves an instance, given an identifier.
        /// </summary>
        /// <param name="id">The identifier that is associated with the content instance you are trying to retrieve.</param>
        /// <returns>The registered content instance that is associated with the given identifier.</returns>
        T? Get(Identifier id);

        /// <summary>
        ///     Get a registered content instance's associated identifier.
        /// </summary>
        /// <param name="entry">The registered content instance.</param>
        /// <returns>The registered content instance's associated identifier.</returns>
        Identifier? GetId(T entry);
    }
}
