using System.ComponentModel;
using Newtonsoft.Json;

namespace HoloCure.Loader
{
    /// <summary>
    ///     JSON template for a mod manifest file.
    /// </summary>
    public sealed class ModFileManifest
    {
        /// <summary>
        ///     Your mod's unique identifier.
        /// </summary>
        [JsonProperty("mod_id")]
        [DefaultValue("")]
        public string ModId { get; set; } = "";

#if COREMODDING
        [JsonProperty("coremod")]
        [DefaultValue(false)]
        public bool Coremod { get; set; } = false;
#endif
    }
}