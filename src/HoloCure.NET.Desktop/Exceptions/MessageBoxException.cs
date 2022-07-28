using System;
using System.Runtime.Serialization;

namespace HoloCure.NET.Desktop.Exceptions
{
    [Serializable]
    public class MessageBoxException : Exception
    {
        public string Title { get; }

        public MessageBoxException() : base("") {
            Title = "Error";
        }

        public MessageBoxException(string message) : base(message) {
            Title = "Error";
        }
        
        public MessageBoxException(string message, Exception inner) : base(message, inner) {
            Title = "Error";
        }
        
        public MessageBoxException(string title, string message, Exception inner) : base(message, inner) {
            Title = title;
        }

        protected MessageBoxException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context) {
            Title = "Serialized Exception";
        }
    }
}