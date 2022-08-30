#region License
// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.
#endregion

namespace HoloCure.Framework.Services
{
    /// <summary>
    ///     A service invoked when a <see cref="FrameworkGame"/> first starts, right before initialization.
    /// </summary>
    public interface IStartUpService
    {
        /// <summary>
        ///     Invoked right before <see cref="FrameworkGame"/> is initialized.
        /// </summary>
        /// <param name="game">The <see cref="FrameworkGame"/> instance.</param>
        void StartUp(FrameworkGame game);
    }
}
