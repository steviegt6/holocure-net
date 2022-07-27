using System.ComponentModel;
using Newtonsoft.Json;

namespace HoloCure.Game.API.Loader
{
    /// <summary>
    ///     A type representing a mod's <c>manifest.json</c> file.
    /// </summary>
    public sealed class ModFileManifest
    {
        // TODO: Check for duplicate IDs.
        [JsonProperty("mod_id")]
        [DefaultValue("")]
        public string ModId { get; } = "";
    }
}
