#region License
// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.
#endregion

using System;
using HoloCure.Framework;
using HoloCure.Framework.Platform;

namespace HoloCure.NET.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args) {
            using FrameworkHost host = FrameworkHost.GetSuitablePlatformHost(HoloCureGame.NAME);
            host.Run(() => new HoloCureGameDesktop());
        }
    }
}
