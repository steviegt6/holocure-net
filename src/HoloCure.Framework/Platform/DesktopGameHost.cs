#region License

// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

#endregion

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace HoloCure.Framework.Platform
{
    public abstract class DesktopGameHost : FrameworkHost
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetDefaultDllDirectories(int directoryFlags);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern void AddDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetDllDirectory(string lpPathName);

        private const int load_library_search_default_dirs = 0x00001000;
        private const string lib_dir = "lib";

        protected DesktopGameHost(string name) : base(name) { }

        protected override void Initialize()
        {
            // https://github.com/FNA-XNA/FNA/wiki/4:-FNA-and-Windows-API#64-bit-support
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                try
                {
                    SetDefaultDllDirectories(load_library_search_default_dirs);
                    AddDllDirectory(Path.Combine(
                                        AppDomain.CurrentDomain.BaseDirectory,
                                        lib_dir,
                                        Environment.Is64BitProcess ? "x64" : "x86"
                                    ));
                }
                catch
                {
                    // Pre-Windows 7, KB2533623
                    SetDllDirectory(Path.Combine(
                                        AppDomain.CurrentDomain.BaseDirectory,
                                        lib_dir,
                                        Environment.Is64BitProcess ? "x64" : "x86"
                                    ));
                }
            }

            // https://github.com/FNA-XNA/FNA/wiki/7:-FNA-Environment-Variables#fna_graphics_enable_highdpi
            // NOTE: from documentation:
            //       Lastly, when packaging for macOS, be sure this is in your app bundle's Info.plist:
            //           <key>NSHighResolutionCapable</key>
            //           <string>True</string>
            Environment.SetEnvironmentVariable("FNA_GRAPHICS_ENABLE_HIGHDPI", "1");
        }
    }
}
