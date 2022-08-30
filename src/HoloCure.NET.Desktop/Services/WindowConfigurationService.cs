#region License
// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.
#endregion

using HoloCure.Framework;
using HoloCure.Framework.Services;

namespace HoloCure.NET.Desktop.Services
{
    public class WindowConfigurationService : IStartUpService
    {
        public void StartUp(FrameworkGame game)
        {
            // HoloCure shows the mouse even though it is not usable.
            game.IsMouseVisible = true;
        }
    }
}
