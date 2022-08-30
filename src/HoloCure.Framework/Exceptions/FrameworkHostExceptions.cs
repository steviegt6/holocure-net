#region License
// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.
#endregion

using System;
using System.Runtime.Serialization;

namespace HoloCure.Framework.Exceptions
{
    [Serializable]
    public class FrameworkHostAlreadyStartedException : Exception
    {
        public FrameworkHostAlreadyStartedException() { }
        public FrameworkHostAlreadyStartedException(string message) : base(message) { }
        public FrameworkHostAlreadyStartedException(string message, Exception inner) : base(message, inner) { }

        protected FrameworkHostAlreadyStartedException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context)
        { }
    }

    [Serializable]
    public class FrameworkHostGameStillRunningException : Exception
    {
        public FrameworkHostGameStillRunningException() { }
        public FrameworkHostGameStillRunningException(string message) : base(message) { }
        public FrameworkHostGameStillRunningException(string message, Exception inner) : base(message, inner) { }

        protected FrameworkHostGameStillRunningException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context)
        { }
    }

    [Serializable]
    public class HostNotYetInitializedException : Exception
    {
        public HostNotYetInitializedException() { }
        public HostNotYetInitializedException(string message) : base(message) { }
        public HostNotYetInitializedException(string message, Exception inner) : base(message, inner) { }

        protected HostNotYetInitializedException(
            SerializationInfo info,
            StreamingContext context
        ) : base(info, context)
        { }
    }
}
