#region License

// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

#endregion

using System;
using HoloCure.Framework.Exceptions;
using HoloCure.Framework.Platform;
using HoloCure.Framework.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;

namespace HoloCure.Framework
{
    /// <summary>
    ///     The core instance behind any game or application.
    /// </summary>
    public abstract class FrameworkGame : Game
    {
        #region FrameworkHost

        /// <summary>
        ///     The <see cref="FrameworkHost"/> instance this game is running under. Starts <see langword="null"/>, but is set before the game starts.
        /// </summary>
        protected FrameworkHost GameHost { get; set; } = null!;

        /// <summary>
        ///     Called by a <see cref="FrameworkHost"/> when this instance is attached to the host.
        /// </summary>
        /// <param name="host"></param>
        public virtual void SetHost(FrameworkHost host)
        {
            GameHost = host;
        }

        #endregion

        #region Start-Up Services

        protected virtual void ConfigureStartUpServices(IServiceCollection collection) { }

        protected virtual void InvokeStartUpServices(IServiceProvider provider)
        {
            foreach (IStartUpService service in provider.GetServices<IStartUpService>())
            {
                service.StartUp(this);
            }
        }

        #endregion

        #region Game Hooks

        /// <summary>
        ///     The service collection that holds game start-up services.
        /// </summary>
        public virtual IServiceCollection? StartUpCollection { get; protected set; }

        /// <summary>
        ///     The service provider that handles start-up services.
        /// </summary>
        public virtual IServiceProvider? StartUpProvider { get; protected set; }

        protected override void Initialize()
        {
            if (GameHost is null) throw new HostNotYetInitializedException("Attempted to initialize game without an attached host!");

            // Configure start-up services.
            StartUpCollection = new ServiceCollection();
            ConfigureStartUpServices(StartUpCollection);

            // Invoke configured start-up services from above.
            StartUpProvider = StartUpCollection.BuildServiceProvider();
            InvokeStartUpServices(StartUpProvider);

            // Run expected game initialization.
            base.Initialize();
        }

        #endregion
    }
}
