using System.IO;
using HoloCure.NET.Desktop.Util;

namespace HoloCure.NET.Desktop.Logging.Writers
{
    public class TemporaryFileLogWriter : FileLogWriter
    {
        private TemporaryFileLogWriter(string path) : base(path) { }

        public TemporaryFileLogWriter(string savePath, string extension) : base(GetLogFileName(savePath, extension)) { }
        
        protected static string GetLogFileName(string savePath, string extension) {
            int depth = 0;

            while (true) {
                string number = depth == 0 ? "" : depth.ToString();
                string path = Path.ChangeExtension(savePath + number, extension);
                FileInfo file = new(path);

                if (!file.Exists) return path;
                if (!file.Locked()) {
                    file.Delete();
                    return path;
                }
                depth++;
            }
        }
    }
}