using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoloCure.NET.API.Loader;

namespace HoloCure.NET.Desktop.Loader
{
    public class TopLevelDirectoryProber : IAssemblyProber
    {
        public string ProbeDir { get; }

        public TopLevelDirectoryProber(string probeDir) {
            Directory.CreateDirectory(probeDir);
            ProbeDir = probeDir;
        }
        
        public IEnumerable<FileInfo> Probe() {
            return new DirectoryInfo(ProbeDir).GetFiles().Where(x => x.Name.StartsWith(DesktopAssemblyLoader.PREFIX) && x.Extension == ".dll");
        }
    }
}