using System.ComponentModel;
using Newtonsoft.Json;

namespace HoloCure.NET.API
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
        public string ModId { get; } = "";
    }
}