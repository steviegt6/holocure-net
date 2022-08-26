using System.IO;

namespace HoloCure.NET.Desktop.Util
{
    public static class FileUtils
    {
        public static bool Locked(this FileInfo file) {
            try {
                Stream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None);
                stream.Close();
                return false;
            }
            catch (IOException) {
                return true;
            }
        }
    }
}