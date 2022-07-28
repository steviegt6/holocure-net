using System;
using System.IO;

namespace HoloCure.NET.Desktop.Launch.Platform
{
    public class WindowsStorageProvider : UnifiedStorageProvider
    {
        public WindowsStorageProvider(string name) : base(name) { }
        
        public override string GetDirectory() {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Name);
        }
    }
}