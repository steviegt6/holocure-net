using System;
using System.IO;

namespace HoloCure.NET.Desktop.Util
{
    public static class PlatformUtils
    {
        public static string GetPlatformDependentStoragePath(string folderName) {
            string path;

            if (OperatingSystem.IsWindows())
                path = GetWindowsStoragePath();
            else if (OperatingSystem.IsMacOS())
                path = GetUnixStoragePath();
            else if (OperatingSystem.IsLinux())
                path = GetUnixStoragePath();
            else
                throw new PlatformNotSupportedException("Cannot resolve a storage path for your platform.");

            string dir = Path.Combine(path, folderName);

            if (File.Exists(dir)) throw new DirectoryNotFoundException("A file with the name \"" + dir + "\" already exists!");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            return dir;
        }

        public static string GetWindowsStoragePath() {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        public static string GetUnixStoragePath() {
            string? xdgPath = Environment.GetEnvironmentVariable("XDG_DATA_HOME");

            return string.IsNullOrEmpty(xdgPath)
                ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".local", "share")
                : xdgPath;
        }
    }
}