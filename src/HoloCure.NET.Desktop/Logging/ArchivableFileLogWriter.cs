using System;
using System.IO;

namespace HoloCure.NET.Desktop.Logging
{
    public class ArchivableFileLogWriter : FileLogWriter
    {
        private ArchivableFileLogWriter(string path) : base(path) { }

        public ArchivableFileLogWriter(string savePath, string exception, DateTime time) : this(GetLogFileName(savePath, exception, time)) { }

        protected static string GetLogFileName(string savePath, string extension, DateTime time) {
            int depth = 0;

            while (true) {
                string number = depth == 0 ? "" : depth.ToString();
                string path = Path.ChangeExtension($"{savePath}-{time:yyyy-M-d-hh-mm-ss}{(number.Length == 0 ? "" : '-')}{depth}", extension);

                if (!File.Exists(path)) return path;
                depth++;
            }
        }
    }
}