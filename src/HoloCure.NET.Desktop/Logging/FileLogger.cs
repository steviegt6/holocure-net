using System.IO;
using HoloCure.NET.Logging;

namespace HoloCure.NET.Desktop.Logging
{
    public abstract class FileLogWriter : ILogWriter
    {
        protected readonly string SavePath;
        protected readonly StreamWriter Writer;

        protected FileLogWriter(string path) {
            SavePath = path;

            Stream stream = File.Open(SavePath, FileMode.Create, FileAccess.Write, FileShare.Read);
            Writer = new StreamWriter(stream)
            {
                AutoFlush = true
            };
        }

        public void Log(string message, ILogLevel level) {
            Writer.WriteLine(message);
        }

        public void Dispose() {
            Writer.Dispose();
        }
    }
}