namespace HoloCure.NET.Screens
{
    /// <summary>
    ///     <see cref="IScreen"/> stack management.
    /// </summary>
    public interface IScreenStack
    {
        /// <summary>
        ///     The current screen.
        /// </summary>
        IScreen? CurrentScreen { get; }

        /// <summary>
        ///     The previous screen.
        /// </summary>
        IScreen? PreviousScreen { get; }

        /// <summary>
        ///     Enter into the given screen.
        /// </summary>
        /// <param name="screen">The screen to enter.</param>
        void EnterScreen(IScreen screen);

        /// <summary>
        ///     Exit the current screen.
        /// </summary>
        /// <returns>True if there was a previous screen, false if there was not.</returns>
        bool ExitScreen();
    }
}