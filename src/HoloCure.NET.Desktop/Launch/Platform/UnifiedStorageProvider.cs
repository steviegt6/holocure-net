﻿using System.IO;
using HoloCure.Core.Launch;

namespace HoloCure.NET.Desktop.Launch.Platform
{
    public abstract class UnifiedStorageProvider : IStorageProvider
    {
        public virtual string Name { get; }
        
        protected UnifiedStorageProvider(string name) {
            Name = name;
        }
        
        public abstract string GetDirectory();

        public virtual DirectoryInfo GetDirectoryInfo() {
            return new DirectoryInfo(GetDirectory());
        }
    }
}