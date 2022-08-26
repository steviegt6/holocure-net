using System.Runtime.Serialization;

namespace HoloCure.EventBus.Exceptions
{
    [Serializable]
    public class TooManyParametersInEventListenerException : Exception
    {
        public TooManyParametersInEventListenerException() { }
        public TooManyParametersInEventListenerException(string message) : base(message) { }
        public TooManyParametersInEventListenerException(string message, Exception inner) : base(message, inner) { }

        protected TooManyParametersInEventListenerException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }

    [Serializable]
    public class NotEnoughParametersInEventListenerException : Exception
    {
        public NotEnoughParametersInEventListenerException() { }
        public NotEnoughParametersInEventListenerException(string message) : base(message) { }
        public NotEnoughParametersInEventListenerException(string message, Exception inner) : base(message, inner) { }

        protected NotEnoughParametersInEventListenerException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }

    [Serializable]
    public class ParameterIsNotEventInEventListenerException : Exception
    {
        public ParameterIsNotEventInEventListenerException() { }
        public ParameterIsNotEventInEventListenerException(string message) : base(message) { }
        public ParameterIsNotEventInEventListenerException(string message, Exception inner) : base(message, inner) { }

        protected ParameterIsNotEventInEventListenerException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) { }
    }
}