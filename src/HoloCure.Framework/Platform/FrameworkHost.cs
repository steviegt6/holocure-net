#region License

// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

#endregion

using System;
using HoloCure.Framework.Exceptions;
using HoloCure.Framework.Platform.Linux;
using HoloCure.Framework.Platform.MacOS;
using HoloCure.Framework.Platform.Windows;

namespace HoloCure.Framework.Platform
{
    /// <summary>
    ///     Handles the initialization and management of a <see cref="FrameworkGame"/>.
    /// </summary>
    public abstract class FrameworkHost : IDisposable
    {
        /// <summary>
        ///     The name of the host.
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     The current host execution state.
        /// </summary>
        public HostState State { get; set; }

        protected FrameworkHost(string name)
        {
            Name = name;
        }

        /// <summary>
        ///     Attaches the host to the game and runs it.
        /// </summary>
        /// <param name="gameFactory">Produces a <see cref="FrameworkGame"/> instance to run.</param>
        /// <exception cref="FrameworkHostAlreadyStartedException">Invoking <see cref="Run"/> if it had already been previously called.</exception>
        public virtual void Run(Func<FrameworkGame> gameFactory)
        {
            if (State != HostState.Idle)
                throw new FrameworkHostAlreadyStartedException($"Attempted to run game under a host which has previously called {nameof(Run)}.");

            State = HostState.Running;

            Initialize();
            runGame(gameFactory.Invoke());

            State = HostState.Stopped;
        }

        /// <summary>
        ///     Invoked in <see cref="Run"/> before the host is attached to the game and before the game is created.
        /// </summary>
        protected abstract void Initialize();

        private void runGame(FrameworkGame game)
        {
            game.SetHost(this);
            game.Run();
            game.Dispose();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (State == HostState.Running)
                throw new FrameworkHostGameStillRunningException("Attempted to dispose of a host that still has a running game instance.");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Retrieves a <see cref="FrameworkHost"/> suitable for the running environment.
        /// </summary>
        /// <param name="name">The framework game/host name.</param>
        /// <exception cref="PlatformNotSupportedException">The current running environment is not supported.</exception>
        public static FrameworkHost GetSuitablePlatformHost(string name)
        {
            if (OperatingSystem.IsWindows()) return new WindowsGameHost(name);
            if (OperatingSystem.IsMacOS()) return new MacOSGameHost(name);
            if (OperatingSystem.IsLinux()) return new LinuxGameHost(name);
            throw new PlatformNotSupportedException("Cannot create a game host for your platform.");
        }
    }
}
