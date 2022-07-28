using System;
using HoloCure.NET.Desktop.Exceptions;
using HoloCure.NET.Desktop.Launch;
using HoloCure.NET.Desktop.Util;
using HoloCure.NET.Launch;
using HoloCure.NET.Logging;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop
{
    public static class Program
    {
        public static void Main(string[] args) {
            ILogger? logger = null;

            try {
                IGameLauncher launcher = CreateLauncher(
#if COREMODDING
                    args.Contains("--skip-coremods")
#endif
                );

                logger = launcher.Logger;
                logger.Log($"Program started with launch arguments: {string.Join(", ", args)}", LogLevels.Debug);

                using Game? game = launcher.LaunchGame(args);

                // The launcher may return null if it's used for other tasks.
                if (game is null) {
                    logger.Log("Launched game was null.", LogLevels.Info);
                    return;
                }

                try {
                    logger.Log("Starting game loop...", LogLevels.Debug);
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
                }
                catch (Exception e) {
                    LogMessageBox(
                        "Fatal Exception (Runtime)",
                        "A fatal exception h as occured and the program may no longer execute.\nStacktrace:\n\n" + e,
                        game.Window.Handle,
                        logger
                    );
                }
            }
            catch (DllNotFoundException e) {
                Console.WriteLine($"Failed to load DLL:\n{e}");
            }
            catch (Exception e) {
                LogMessageBox(
                    "Fatal Exception (Launch)",
                    "A fatal exception has occured whilst launching the game and the program may no longer execute.\nStacktrace:\n\n" + e,
                    IntPtr.Zero,
                    logger
                );
            }
        }
        
        private static void LogMessageBox(string title, string message, IntPtr handle, ILogger? logger) {
            if (logger is null) message = "CRASH OCCURED PRIOR TO A LOGGER BEING INITIALIZED, THIS ERROR HAS NOT BEEN LOGGED ANYWHERE.\n\n" + message;
            MessageBox.MakeError_Simple(title, message, handle);
            logger?.Log(message, LogLevels.Fatal);
        }

        public static IGameLauncher CreateLauncher(
#if COREMODDING
            bool skipCoremods
#endif
        ) {
            IGameLauncher launcher =
#if COREMODDING
                skipCoremods ? new DesktopGameLauncher() : new CoremodLauncher();
#else
                new DesktopGameLauncher();
#endif
            return launcher;
        }
    }
}