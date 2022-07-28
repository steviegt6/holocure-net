using System;

namespace HoloCure.NET.Desktop.Util
{
    public static class MessageBox
    {
        public static int MakeError_Simple(string title, string message, IntPtr parentWindowHandle) {
            return SDL2.SDL.SDL_ShowSimpleMessageBox(SDL2.SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_ERROR, title, message, parentWindowHandle);
        }
    }
}