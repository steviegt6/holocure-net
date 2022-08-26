using System;
using System.Runtime.Serialization;

namespace HoloCure.NET.Desktop.Exceptions
{
    [Serializable]
    public abstract class ModLoadException : Exception
    {
        public string Mod { get; }

        protected ModLoadException(string mod) { Mod = mod; }
        protected ModLoadException(string mod, string message) : base(message) { Mod = mod; }
        protected ModLoadException(string mod, string message, Exception inner) : base(message, inner) { Mod = mod; }
        protected ModLoadException(SerializationInfo info, StreamingContext context) : base(info, context) { Mod = "Serialized"; }
    }

    [Serializable]
    public class ModLoadMissingAssemblyException : ModLoadException
    {
        public ModLoadMissingAssemblyException(string mod) : base(mod) { }
        public ModLoadMissingAssemblyException(string mod, string message) : base(mod, message) { }
        public ModLoadMissingAssemblyException(string mod, string message, Exception inner) : base(mod, message, inner) { }
        protected ModLoadMissingAssemblyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ModLoadMissingManifestException : ModLoadException
    {
        public ModLoadMissingManifestException(string mod) : base(mod) { }
        public ModLoadMissingManifestException(string mod, string message) : base(mod, message) { }
        public ModLoadMissingManifestException(string mod, string message, Exception inner) : base(mod, message, inner) { }
        protected ModLoadMissingManifestException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ModLoadMissingModInterfaceException : ModLoadException
    {
        public ModLoadMissingModInterfaceException(string mod) : base(mod) { }
        public ModLoadMissingModInterfaceException(string mod, string message) : base(mod, message) { }
        public ModLoadMissingModInterfaceException(string mod, string message, Exception inner) : base(mod, message, inner) { }
        protected ModLoadMissingModInterfaceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class ModLoadMultipleModInterfacesException : ModLoadException
    {
        public ModLoadMultipleModInterfacesException(string mod) : base(mod) { }
        public ModLoadMultipleModInterfacesException(string mod, string message) : base(mod, message) { }
        public ModLoadMultipleModInterfacesException(string mod, string message, Exception inner) : base(mod, message, inner) { }
        protected ModLoadMultipleModInterfacesException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}