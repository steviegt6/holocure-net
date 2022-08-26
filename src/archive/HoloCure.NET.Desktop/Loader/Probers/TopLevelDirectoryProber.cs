#if false
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HoloCure.Loader;

namespace HoloCure.NET.Desktop.Loader.Probers
{
    public class TopLevelDirectoryProber : IAssemblyProber
    {
        public string ProbeDir { get; }

        public TopLevelDirectoryProber(string probeDir) {
            Directory.CreateDirectory(probeDir);
            ProbeDir = probeDir;
        }
        
        public IEnumerable<FileInfo> Probe() {
            return new DirectoryInfo(ProbeDir).GetFiles().Where(x => x.Name.StartsWith(AssemblyLoader.PREFIX) && x.Extension == ".dll");
        }
    }
}
#endif