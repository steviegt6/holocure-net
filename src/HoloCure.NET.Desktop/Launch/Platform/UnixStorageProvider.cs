using System;
using System.IO;

namespace HoloCure.NET.Desktop.Launch.Platform
{
    public abstract class UnixStorageProvider : UnifiedStorageProvider
    {
        protected UnixStorageProvider(string name) : base(name) { }

        public override string GetDirectory() {
            static string GetBaseDirectory() {
                string? xdgPath = Environment.GetEnvironmentVariable("XDG_DATA_HOME");

                return string.IsNullOrEmpty(xdgPath)
                    ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".local", "share")
                    : xdgPath;
            }

            return Path.Combine(GetBaseDirectory(), Name);
        }
    }
}