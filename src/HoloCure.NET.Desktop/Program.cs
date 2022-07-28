using System;
using HoloCure.NET.Desktop.Launch;
using HoloCure.NET.Desktop.Util;
using HoloCure.NET.Launch;
using Microsoft.Xna.Framework;

namespace HoloCure.NET.Desktop
{
    public static class Program
    {
        public static void Main(string[] args) {
            try {
                IGameLauncher launcher = CreateLauncher(
#if COREMODDING
                    args.Contains("--skip-coremods")
#endif
                );
                using Game? game = launcher.LaunchGame(args);

                // The launcher may return null if it's used for other tasks.
                if (game is null) return;

                try {
                    game.Run();
                }
                catch (Exception e) {
                    SDL2.SDL.SDL_ShowSimpleMessageBox(
                        SDL2.SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR,
                        "Fatal Exception (Runtime)",
                        "A fatal exception has occured and the program may no longer execute. A full stack trace is below:\n\n" + e,
                        game.Window.Handle
                    );
                }
            }
            catch (Exception e) {
                MessageBox.MakeError_Simple(
                    "Fatal Exception (Launch)",
                    "A fatal exception has occured whilst launching the game and the program may no longer execute. A full stack trace is below:\n\n" + e,
                    IntPtr.Zero
                );
            }
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