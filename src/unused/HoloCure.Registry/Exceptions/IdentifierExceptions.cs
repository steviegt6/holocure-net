﻿using System.Runtime.Serialization;

namespace HoloCure.Registry.Exceptions
{
    [Serializable]
    public class IdentifierParseException : Exception
    {
        public IdentifierParseException() { }
        public IdentifierParseException(string message) : base(message) { }
        public IdentifierParseException(string message, Exception inner) : base(message, inner) { }

        protected IdentifierParseException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }
}