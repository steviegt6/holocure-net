using System.Collections.Generic;
using HoloCure.NET.API.Exceptions;

namespace HoloCure.NET.API.Registry
{
    /// <summary>
    ///     A mutable implementation of <see cref="IRegistrar"/>.
    /// </summary>
    public class MutableRegistrar : IRegistrar
    {
        public IDictionary<Identifier, object> RegisteredContent { get; } = new Dictionary<Identifier, object>();

        public IDictionary<object, Identifier> ReverseLookup { get; } = new Dictionary<object, Identifier>();

        public object Register(Identifier id, object entry) {
            if (RegisteredContent.ContainsKey(id))
                throw new RegistrarDuplicateKeyException("Tried to register content under an identifier that is already registered: " + id);

            RegisteredContent.Add(id, entry);
            ReverseLookup.Add(entry, id);
            return entry;
        }

        public object? Get(Identifier id) {
            return RegisteredContent.ContainsKey(id) ? RegisteredContent[id] : default;
        }

        public Identifier? GetId(object entry) {
            return ReverseLookup.ContainsKey(entry) ? ReverseLookup[entry] : default;
        }
    }

    /// <summary>
    ///     A mutable implementation of <see cref="IRegistrar{T}"/>.
    /// </summary>
    public class MutableRegistrar<T> : IRegistrar<T>
        where T : notnull
    {
        public IDictionary<Identifier, T> RegisteredContent { get; } = new Dictionary<Identifier, T>();

        public IDictionary<T, Identifier> ReverseLookup { get; } = new Dictionary<T, Identifier>();

        public T Register(Identifier id, T entry) {
            if (RegisteredContent.ContainsKey(id))
                throw new RegistrarDuplicateKeyException("Tried to register content under an identifier that is already registered: " + id);

            RegisteredContent.Add(id, entry);
            ReverseLookup.Add(entry, id);
            return entry;
        }

        public T? Get(Identifier id) {
            return RegisteredContent.ContainsKey(id) ? RegisteredContent[id] : default;
        }

        public Identifier? GetId(T entry) {
            return ReverseLookup.ContainsKey(entry) ? ReverseLookup[entry] : default;
        }
    }
}