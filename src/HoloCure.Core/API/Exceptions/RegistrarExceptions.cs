using System;
using System.Runtime.Serialization;

namespace HoloCure.Core.API.Exceptions
{
    [Serializable]
    public class ImmutableRegistrarException : Exception
    {
        public ImmutableRegistrarException() { }
        public ImmutableRegistrarException(string message) : base(message) { }
        public ImmutableRegistrarException(string message, Exception inner) : base(message, inner) { }

        protected ImmutableRegistrarException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }

    [Serializable]
    public class RegistrarDuplicateKeyException : Exception
    {
        public RegistrarDuplicateKeyException() { }
        public RegistrarDuplicateKeyException(string message) : base(message) { }
        public RegistrarDuplicateKeyException(string message, Exception inner) : base(message, inner) { }

        protected RegistrarDuplicateKeyException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }
}