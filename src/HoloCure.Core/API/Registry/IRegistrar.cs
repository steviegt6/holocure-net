using System.Collections.Generic;
using System.Linq;

namespace HoloCure.Core.API.Registry
{
    /// <summary>
    ///     A singleton-based content registrar.
    /// </summary>
    public interface IRegistrar
    {
        /// <summary>
        ///     A dictionary mapping identifiers to their associated content.
        /// </summary>
        /// <remarks>
        ///     This dictionary may be immutable and should not be used for direct lookups under normal circumstances. <br />
        ///     When retrieving and adding content, you should use <see cref="Register"/> and <see cref="Get"/>.
        /// </remarks>
        IDictionary<Identifier, object> RegisteredContent { get; }

        /// <summary>
        ///     Reverse lookup mapping content to an associated identifier.
        /// </summary>
        /// <remarks>
        ///     The same caveats that apply to <see cref="RegisteredContent"/> apply to this property. <br />
        ///     You should use <see cref="GetId"/> for retrieval, and should never try to write to this dictionary from an external object.
        /// </remarks>
        IDictionary<object, Identifier> ReverseLookup { get; }

        /// <summary>
        ///     Registers an instance, given an identifier.
        /// </summary>
        /// <param name="id">The identifier to register this instance under.</param>
        /// <param name="entry">The content instance to be registered.</param>
        /// <returns>The now-registered content instance.</returns>
        object Register(Identifier id, object entry);

        /// <summary>
        ///     Retrieves an instance, given an identifier.
        /// </summary>
        /// <param name="id">The identifier associated with the content instance you want to retrieve.</param>
        /// <returns>The content instance associated with the provided identifier. Returns null if no content is present.</returns>
        object? Get(Identifier id);

        /// <summary>
        ///     Retrieves a content instance's associated identifier.
        /// </summary>
        /// <param name="entry">The content instance associated with the identifier you want to retrieve.</param>
        /// <returns>The identifier associated with the provided content instance. Returns null if no identifier is present.</returns>
        Identifier? GetId(object entry);
    }

    /// <inheritdoc cref="IRegistrar"/>
    /// <typeparam name="T">The content type.</typeparam>
    public interface IRegistrar<T> : IRegistrar
        where T : notnull
    {
        #region IRegistrar Implementation

        IDictionary<Identifier, object> IRegistrar.RegisteredContent => RegisteredContent.ToDictionary(x => x.Key, x => (object) x.Value);

        IDictionary<object, Identifier> IRegistrar.ReverseLookup => ReverseLookup.ToDictionary(x => (object) x.Key, x => x.Value);

        object IRegistrar.Register(Identifier id, object entry) {
            return Register(id, (T) entry);
        }

        object? IRegistrar.Get(Identifier id) {
            return Get(id);
        }

        Identifier? IRegistrar.GetId(object entry) {
            return GetId((T) entry);
        }

        #endregion

        /// <summary>
        ///     A dictionary mapping identifiers to their associated content.
        /// </summary>
        /// <remarks>
        ///     This dictionary may be immutable and should not be used for direct lookups under normal circumstances. <br />
        ///     When retrieving and adding content, you should use <see cref="Register"/> and <see cref="Get"/>.
        /// </remarks>
        new IDictionary<Identifier, T> RegisteredContent { get; }

        /// <summary>
        ///     Reverse lookup mapping content to an associated identifier.
        /// </summary>
        /// <remarks>
        ///     The same caveats that apply to <see cref="RegisteredContent"/> apply to this property. <br />
        ///     You should use <see cref="GetId"/> for retrieval, and should never try to write to this dictionary from an external object.
        /// </remarks>
        new IDictionary<T, Identifier> ReverseLookup { get; }

        /// <summary>
        ///     Registers an instance, given an identifier.
        /// </summary>
        /// <param name="id">The identifier to register this instance under.</param>
        /// <param name="entry">The content instance to be registered.</param>
        /// <returns>The now-registered content instance.</returns>
        T Register(Identifier id, T entry);

        /// <summary>
        ///     Retrieves an instance, given an identifier.
        /// </summary>
        /// <param name="id">The identifier associated with the content instance you want to retrieve.</param>
        /// <returns>The content instance associated with the provided identifier. Returns null if no content is present.</returns>
        new T? Get(Identifier id);

        /// <summary>
        ///     Retrieves a content instance's associated identifier.
        /// </summary>
        /// <param name="entry">The content instance associated with the identifier you want to retrieve.</param>
        /// <returns>The identifier associated with the provided content instance. Returns null if no identifier is present.</returns>
        Identifier? GetId(T entry);
    }
}