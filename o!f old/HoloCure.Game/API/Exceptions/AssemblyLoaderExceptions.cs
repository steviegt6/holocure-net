using System;
using System.Runtime.Serialization;

namespace HoloCure.Game.API.Exceptions
{
    [Serializable]
    public class NullManifestException : Exception
    {
        public NullManifestException() { }

        public NullManifestException(string message)
            : base(message)
        {
        }

        public NullManifestException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected NullManifestException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class MissingManifestFileException : Exception
    {
        public MissingManifestFileException() { }

        public MissingManifestFileException(string message)
            : base(message)
        {
        }

        public MissingManifestFileException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected MissingManifestFileException(
            SerializationInfo info,
            StreamingContext context
        )
            : base(info, context)
        {
        }
    }
}
