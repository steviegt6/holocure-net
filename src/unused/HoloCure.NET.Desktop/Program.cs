using System;
using System.IO;
using HoloCure.Logging;
using HoloCure.Logging.Levels;
using HoloCure.NET.Desktop.Logging;
using HoloCure.NET.Desktop.Logging.Writers;
using HoloCure.NET.Desktop.Util;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args) {
            // Initialize FNA first, we want to be able to reliably load SDL for message boxes.
            // After that, immediately create a storage provider and logger - if that fails, we cannot do much.
            try {
                FnaBootstrap.Initialize_FNA();
            }
            catch {
                // We sadly cannot display this in a message box. Initialize_FNA will never realistically throw, though.
                // It's possible for FNA to be unresolved, but that won't be caught or even known about here, it'll happen later.
                Console.WriteLine("Failed to initialize FNA native dependency paths.");
                throw;
            }

            string saveDir;
            GameLogger.SourceLogger logger;

            try {
                saveDir = PlatformUtils.GetPlatformDependentStoragePath("HoloCure");
                GameLogger.InitializeLogger(
                    new DesktopLogger(
                        // Log to the console.
                        new ConsoleLogWriter(),
                        // Log to an archivable log file (one that won't be cleared).
                        new ArchivableFileLogWriter(Path.Combine(saveDir, "Logs", "Archive", "game"), ".log", DateTime.Now),
                        // Log to a temporary log file ("latest.log")
                        new TemporaryFileLogWriter(Path.Combine(saveDir, "Logs", "latest"), ".log")
                    )
                );
                logger = GameLogger.MakeLogger("Launcher");
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
                logger.Log("Instantiating Game object...", LogLevels.Debug);
                game = new HoloCureGame(saveDir);
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

            try {
                logger.Log("Entering game loop...", LogLevels.Debug);
                game.Run();
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

            logger.Log("Exited game loop, assuming safe exit. Disposing of Game instance...", LogLevels.Debug);
            game.Dispose();

            logger.Log("Disposing of logger...", LogLevels.Debug);
            GameLogger.DisposeLogger();
        }

        private static void LogMessageBox(string title, string message, IntPtr handle, ILogger logger) {
            MessageBox.MakeError_Simple(title, message, handle);
            logger.Log($"{title}: {message}", LogLevels.Fatal);
        }

        // args.Contains("--skip-coremods")
        // bool skipCoremods
        // new CoremodLauncher()
        /*public static IGameLauncher CreateLauncher(IStorageProvider storageProvider, ILogger logger, string[] args) {
            return new DesktopGameLauncher(storageProvider, logger, args);
        }*/
    }
}