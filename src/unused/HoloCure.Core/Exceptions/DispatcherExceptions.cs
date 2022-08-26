using System;
using System.Runtime.Serialization;

namespace HoloCure.Core.Exceptions
{
    [Serializable]
    public class DuplicateGameSystemInDispatcherException : Exception
    {
        public DuplicateGameSystemInDispatcherException() { }
        public DuplicateGameSystemInDispatcherException(string message) : base(message) { }
        public DuplicateGameSystemInDispatcherException(string message, Exception inner) : base(message, inner) { }

        protected DuplicateGameSystemInDispatcherException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }
}