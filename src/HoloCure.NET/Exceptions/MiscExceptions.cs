using System;
using System.Runtime.Serialization;

namespace HoloCure.NET.Exceptions
{
   [Serializable]
   public class LoggerNotInitializedException : Exception
   {
      public LoggerNotInitializedException() { }
      public LoggerNotInitializedException(string message) : base(message) { }
      public LoggerNotInitializedException(string message, Exception inner) : base(message, inner) { }

      protected LoggerNotInitializedException(
         SerializationInfo info,
         StreamingContext context
      ) : base(info, context) { }
   }
   
   [Serializable]
   public class LoggerAlreadyInitializedException : Exception
   {
      public LoggerAlreadyInitializedException() { }
      public LoggerAlreadyInitializedException(string message) : base(message) { }
      public LoggerAlreadyInitializedException(string message, Exception inner) : base(message, inner) { }

      protected LoggerAlreadyInitializedException(
         SerializationInfo info,
         StreamingContext context
      ) : base(info, context) { }
   }
}