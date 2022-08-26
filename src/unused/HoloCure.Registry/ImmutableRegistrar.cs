using System.Collections.ObjectModel;
using HoloCure.Registry.Exceptions;

namespace HoloCure.Registry
{
    /// <summary>
    ///     An immutable implementation of <see cref="IRegistrar"/>.
    /// </summary>
    public class ImmutableRegistrar : IRegistrar
    {
        public IDictionary<Identifier, object> RegisteredContent { get; }

        public IDictionary<object, Identifier> ReverseLookup { get; }

        /// <summary>
        ///     Creates an immutable registrar for an existing registrar (the mutability of which is irrelevant), in which all registered content is preserved but no further content may be added.
        /// </summary>
        /// <param name="registrar">The original registrar to preserve content from.</param>
        public ImmutableRegistrar(IRegistrar registrar) {
            RegisteredContent = new ReadOnlyDictionary<Identifier, object>(registrar.RegisteredContent);
            ReverseLookup = new ReadOnlyDictionary<object, Identifier>(registrar.ReverseLookup);
        }

        public object Register(Identifier id, object entry) {
            throw new ImmutableRegistrarException($"Attempted to register content with an ID of \"{id}\" to an immutable registrar!");
        }

        public object? Get(Identifier id) {
            return RegisteredContent.ContainsKey(id) ? RegisteredContent[id] : default;
        }

        public Identifier? GetId(object entry) {
            return ReverseLookup.ContainsKey(entry) ? ReverseLookup[entry] : default;
        }
    }

    /// <summary>
    ///     An immutable implementation of <see cref="IRegistrar{T}"/>.
    /// </summary>
    public class ImmutableRegistrar<T> : IRegistrar<T>
        where T : notnull
    {
        public IDictionary<Identifier, T> RegisteredContent { get; }

        public IDictionary<T, Identifier> ReverseLookup { get; }

        /// <summary>
        ///     Creates an immutable registrar for an existing registrar (the mutability of which is irrelevant), in which all registered content is preserved but no further content may be added.
        /// </summary>
        /// <param name="registrar">The original registrar to preserve content from.</param>
        public ImmutableRegistrar(IRegistrar<T> registrar) {
            RegisteredContent = new ReadOnlyDictionary<Identifier, T>(registrar.RegisteredContent);
            ReverseLookup = new ReadOnlyDictionary<T, Identifier>(registrar.ReverseLookup);
        }

        public T Register(Identifier id, T entry) {
            throw new ImmutableRegistrarException($"Attempted to register content with an ID of \"{id}\" to an immutable registrar!");
        }

        public T? Get(Identifier id) {
            return RegisteredContent.ContainsKey(id) ? RegisteredContent[id] : default;
        }

        public Identifier? GetId(T entry) {
            return ReverseLookup.ContainsKey(entry) ? ReverseLookup[entry] : default;
        }
    }
}