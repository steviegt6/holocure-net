using System.Collections.Generic;
using System.Drawing;
using HoloCure.Game.API.Loader;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;
using HoloCure.Resources;
using osu.Framework.Configuration;

namespace HoloCure.Game
{
    // Anything in this class is shared between the test browser and the game implementation.
    // It allows for caching global dependencies that should be accessible to tests, or changing
    // the screen scaling for all components including the test browser and framework overlays.
    public class HoloCureGameBase : osu.Framework.Game
    {
        public const string GAME_NAME = "HoloCure";

        /// <summary>
        ///     The display width, used for absolute positioning.
        /// </summary>
        public const int DISPLAY_WIDTH = 640;

        /// <summary>
        ///     The display height, used for absolute positioning.
        /// </summary>
        public const int DISPLAY_HEIGHT = 360;

        public const int DEFAULT_WIDTH = DISPLAY_WIDTH * 2;

        public const int DEFAULT_HEIGHT = DISPLAY_HEIGHT * 2;

        /// <summary>
        ///     Display size ratio, width over height.
        /// </summary>
        public const float DISPLAY_RATIO = (float)DISPLAY_WIDTH / DISPLAY_HEIGHT;

        protected override Container<Drawable> Content => content;

        protected IAssemblyLoader AssemblyLoader { get; private set; } = null!;

        private Container content = null!;
        private DependencyContainer dependencies = null!;

        protected HoloCureGameBase()
        {
            Name = GAME_NAME;
            RelativeSizeAxes = Axes.Both;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(HoloCureResources).Assembly));

            dependencies.Cache(AssemblyLoader = new HoloCureAssemblyLoader());

            base.Content.Add(content = CreateContainer());
        }

        protected virtual Container CreateContainer()
        {
            return new DrawSizePreservingFillContainer
            {
                TargetDrawSize = new Vector2(DISPLAY_WIDTH, DISPLAY_HEIGHT),
                Strategy = DrawSizePreservationStrategy.Separate
            };
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));

        protected override IDictionary<FrameworkSetting, object> GetFrameworkConfigDefaults()
        {
            IDictionary<FrameworkSetting, object> defaults = base.GetFrameworkConfigDefaults() ?? new Dictionary<FrameworkSetting, object>();

            defaults[FrameworkSetting.WindowMode] = WindowMode.Windowed;
            defaults[FrameworkSetting.WindowedSize] = new Size(DEFAULT_WIDTH, DEFAULT_HEIGHT);

            return defaults;
        }
    }
}
