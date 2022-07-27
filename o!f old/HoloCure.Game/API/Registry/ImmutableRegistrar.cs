using System.Collections.Generic;
using System.Collections.ObjectModel;
using HoloCure.Game.API.Exceptions;

namespace HoloCure.Game.API.Registry
{
    /// <inheritdoc />
    /// <summary>
    ///     An immutable implementation of <see cref="T:HoloCure.Game.API.Registry.IRegistrar`1" />
    /// </summary>
    public class ImmutableRegistrar<T> : IRegistrar<T>
        where T : notnull
    {
        public IDictionary<Identifier, T> RegisteredContent { get; }

        public IDictionary<T, Identifier> ReverseLookup { get; }

        /// <summary>
        ///     Create an immutable registrar from an existing registrar, in which all registered content is preserved but no further content may be added.
        /// </summary>
        /// <param name="registrar">The original registrar to preserve content from.</param>
        public ImmutableRegistrar(IRegistrar<T> registrar)
        {
            RegisteredContent = new ReadOnlyDictionary<Identifier, T>(registrar.RegisteredContent);
            ReverseLookup = new ReadOnlyDictionary<T, Identifier>(registrar.ReverseLookup);
        }

        public virtual T Register(Identifier id, T entry) => throw new ImmutableRegistrarException($"Attempted to register \"{id}\" to an immutable registry.");

        public virtual T? Get(Identifier id) => RegisteredContent.ContainsKey(id) ? RegisteredContent[id] : default;

        public virtual Identifier? GetId(T entry) => ReverseLookup.ContainsKey(entry) ? ReverseLookup[entry] : default;
    }
}
