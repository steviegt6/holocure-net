using System;
using System.Runtime.Serialization;

namespace HoloCure.Reflection.Exceptions
{
    [Serializable]
    public class GeneratedMethodInvocationException : Exception
    {
        public GeneratedMethodInvocationException() { }
        public GeneratedMethodInvocationException(string message) : base(message) { }
        public GeneratedMethodInvocationException(string message, Exception inner) : base(message, inner) { }

        protected GeneratedMethodInvocationException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }
}