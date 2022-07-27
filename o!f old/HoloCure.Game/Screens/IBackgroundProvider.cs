using osu.Framework.Screens;

namespace HoloCure.Game.Screens
{
    /// <summary>
    ///     A provider for pushing background screen instances.
    /// </summary>
    public interface IBackgroundProvider : IScreen
    {
        /// <summary>
        ///     Get a background screen to switch to.
        /// </summary>
        /// <param name="current">The current background screen.</param>
        /// <returns>The background screen that should be pushed.</returns>
        IScreen GetBackgroundScreen(IScreen current);
    }
}
