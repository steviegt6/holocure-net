using System;
using System.IO;
using HoloCure.NET.Desktop.Launch.Platform;
using HoloCure.NET.Launch;

namespace HoloCure.NET.Desktop.Util
{
    public static class PlatformUtils
    {
        public static IStorageProvider MakePlatformDependentStorageProvider(string folderName) {
            IStorageProvider provider;

            if (OperatingSystem.IsWindows())
                provider = new WindowsStorageProvider(folderName);
            else if (OperatingSystem.IsMacOS())
                provider = new MacStorageProvider(folderName);
            else if (OperatingSystem.IsLinux())
                provider = new LinuxStorageProvider(folderName);
            else
                throw new PlatformNotSupportedException("Cannot create a storage provider for your operating system.");
            
            string dir = provider.GetDirectory();

            if (File.Exists(dir)) throw new DirectoryNotFoundException("A file with the name \"" + dir + "\" already exists!");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            return provider;
        }
    }
}