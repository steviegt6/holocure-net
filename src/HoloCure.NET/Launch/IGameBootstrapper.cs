namespace HoloCure.NET.Launch
{
    /// <summary>
    ///     Bootstraps a game prior to starting.
    /// </summary>
    public interface IGameBootstrapper
    {
        /// <summary>
        ///     Bootstraps the game.
        /// </summary>
        /// <param name="launcher">The launcher this bootstrapper belongs to.</param>
        void Bootstrap(IGameLauncher launcher);
    }
}