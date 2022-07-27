using System.Collections.Generic;
using System.Drawing;
using osu.Framework.Configuration;
using osu.Framework.Platform;

namespace HoloCure.Game.Configuration
{
    // TODO: This mirrors the settings present in the original game. These are mostly relevant to the engine. Should any be moved to a mod?
    public class HoloCureConfigManager : IniConfigManager<HoloCureSetting>
    {
        public HoloCureConfigManager(Storage storage, IDictionary<HoloCureSetting, object>? defaultOverrides = null)
            : base(storage, defaultOverrides)
        {
        }

        protected override void InitialiseDefaults()
        {
            SetDefault(HoloCureSetting.Resolution, new Size(HoloCureGameBase.DEFAULT_WIDTH, HoloCureGameBase.DEFAULT_HEIGHT));
            SetDefault(HoloCureSetting.Fullscreen, false);
            SetDefault(HoloCureSetting.MusicVolume, 0.7f);
            SetDefault(HoloCureSetting.SoundVolume, 1.0f);
            SetDefault(HoloCureSetting.DamageNumbers, true);
            SetDefault(HoloCureSetting.VisualEffects, true);
            SetDefault(HoloCureSetting.Screenshake, true);
            SetDefault(HoloCureSetting.Language, "en");
            SetDefault(HoloCureSetting.HiscoreNames, true);
        }
    }

    // TODO: Keybinds
    public enum HoloCureSetting
    {
        Resolution,
        Fullscreen,
        MusicVolume,
        SoundVolume,
        DamageNumbers,
        VisualEffects,
        Screenshake,
        Language,
        HiscoreNames
    }
}
