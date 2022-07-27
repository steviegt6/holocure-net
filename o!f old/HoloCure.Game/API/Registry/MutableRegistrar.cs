using System.Collections.Generic;
using HoloCure.Game.API.Exceptions;

namespace HoloCure.Game.API.Registry
{
    /// <inheritdoc />
    /// <summary>
    ///     A mutable implementation of <see cref="T:HoloCure.Game.API.Registry.IRegistrar`1" />.
    /// </summary>
    public class MutableRegistrar<T> : IRegistrar<T>
        where T : notnull
    {
        public IDictionary<Identifier, T> RegisteredContent { get; } = new Dictionary<Identifier, T>();

        public IDictionary<T, Identifier> ReverseLookup { get; } = new Dictionary<T, Identifier>();

        public T Register(Identifier id, T entry)
        {
            if (RegisteredContent.ContainsKey(id)) throw new RegistrarDuplicateKeyException("Tried to register content with a duplicate identifier: " + id);

            RegisteredContent.Add(id, entry);
            return entry;
        }

        public T? Get(Identifier id) => RegisteredContent.ContainsKey(id) ? RegisteredContent[id] : default;

        public virtual Identifier? GetId(T entry) => ReverseLookup.ContainsKey(entry) ? ReverseLookup[entry] : default;
    }
}
