using System;
using System.Diagnostics;
using System.IO;
using HoloCure.Core.Launch;
using HoloCure.EventBus;
using HoloCure.EventBus.Attributes;
using HoloCure.EventBus.Extensions;
using HoloCure.Logging;
using HoloCure.Logging.Levels;
using HoloCure.NET.Desktop.Exceptions;
using HoloCure.NET.Desktop.Launch;
using HoloCure.NET.Desktop.Logging;
using HoloCure.NET.Desktop.Logging.Writers;
using HoloCure.NET.Desktop.Util;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop
{
    public static class Program
    {
        public class TestEvent : IEvent { }

        public static int A;


        [Subscriber]
        public static void TestEventListener1(TestEvent theEvent) {
            A++;
        }
        
        [Subscriber]
        public static void TestEventListener2(TestEvent theEvent) {
            A++;
        }
        
        [Subscriber]
        public static void TestEventListener3(TestEvent theEvent) {
            A++;
        }
        
        [Subscriber]
        public static void TestEventListener4(TestEvent theEvent) {
            A++;
        }
        
        [Subscriber]
        public static void TestEventListener5(TestEvent theEvent) {
            A++;
        }
        
        [Subscriber]
        public static void TestEventListener6(TestEvent theEvent) {
            A++;
        }
        
        public static void Main(string[] args) {
            Stopwatch sw = new();
            sw.Start();
            IEventBus bus = new SimpleEventBus();
            sw.Stop();
            Console.WriteLine("Init: " + sw.ElapsedMilliseconds);
            
            sw.Restart();
            bus.RegisterStaticType(typeof(Program));
            sw.Stop();
            Console.WriteLine("Register: " + sw.ElapsedMilliseconds);
            
            sw.Restart();
            for (int i = 0; i < 1000; i++) {
                bus.DispatchEvent(new TestEvent());
            }
            sw.Stop();
            Console.WriteLine("Dispatch: " + sw.ElapsedMilliseconds);
            return;
            // Initialize FNA first, we want to be able to reliably load SDL for message boxes.
            // After that, immediately create a storage provider and logger - if that fails, we cannot do much.
            IStorageProvider storageProvider;
            ILogger logger;

            try {
                FnaBootstrap.Initialize_FNA();
            }
            catch {
                // We sadly cannot display this in a message box. Initialize_FNA will never realistically throw, though.
                // It's possible for FNA to be unresolved, but that won't be caught or even known about here, it'll happen later.
                Console.WriteLine("Failed to initialize FNA native dependency paths.");
                throw;
            }

            try {
                storageProvider = PlatformUtils.MakePlatformDependentStorageProvider(DesktopGameLauncher.GAME_NAME);
                logger = new DesktopLogger(
                    // Log to the console.
                    new ConsoleLogWriter(),
                    // Log to an archivable log file (one that won't be cleared).
                    new ArchivableFileLogWriter(Path.Combine(storageProvider.GetDirectory(), "game"), ".log", DateTime.Now),
                    // Log to a temporary log file ("latest.log")
                    new TemporaryFileLogWriter(Path.Combine(storageProvider.GetDirectory(), "latest"), ".log")
                );
            }
            catch (Exception e) {
                MessageBox.MakeError_Simple(
                    "Fatal Exception (Initialization)",
                    "An exception has occured while initializing the logger and storage provider.\nStacktrace:\n\n" + e,
                    IntPtr.Zero
                );
                throw;
            }

            logger.Log($"Program started with launch arguments: {string.Join(", ", args)}", LogLevels.Debug);

            Game? game;

            try {
                logger.Log("Creating launcher...", LogLevels.Debug);
                IGameLauncher launcher = CreateLauncher(storageProvider, logger, args);
                
                logger.Log("Launching game...", LogLevels.Debug);
                game = launcher.LaunchGame(args);
            }
            catch (Exception e) {
                LogMessageBox(
                    "Fatal Exception (Launch)",
                    "A fatal exception has occured whilst launching the game and the program may no longer execute.\nStacktrace:\n\n" + e,
                    IntPtr.Zero,
                    logger
                );
                throw;
            }

            // The launcher may return null if it's used for other tasks.
            if (game is null) {
                logger.Log("Launched game was null.", LogLevels.Info);
                return;
            }

            try {
                logger.Log("Entering game loop...", LogLevels.Debug);
                game.Run();
            }
            catch (MessageBoxException e) {
                Exception inner = e.InnerException ?? e;
                LogMessageBox(
                    e.Title,
                    e.Message + "\nStacktrace:\n" + inner.StackTrace,
                    game.Window.Handle,
                    logger
                );
                throw inner;
            }
            catch (Exception e) {
                LogMessageBox(
                    "Fatal Exception (Runtime)",
                    "A fatal exception has occured and the program may no longer execute.\nStacktrace:\n\n" + e,
                    game.Window.Handle,
                    logger
                );
                throw;
            }
        }

        private static void LogMessageBox(string title, string message, IntPtr handle, ILogger logger) {
            MessageBox.MakeError_Simple(title, message, handle);
            logger.Log($"{title}: {message}", LogLevels.Fatal);
        }

        // args.Contains("--skip-coremods")
        // bool skipCoremods
        // new CoremodLauncher()
        public static IGameLauncher CreateLauncher(IStorageProvider storageProvider, ILogger logger, string[] args) {
            return new DesktopGameLauncher(storageProvider, logger, args);
        }
    }
}