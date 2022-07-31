using System;
using System.IO;
using System.Linq;
using HoloCure.Core.API;
using HoloCure.NET.Desktop.Loader;
using Newtonsoft.Json;

namespace HoloCure.NET.Desktop.Util
{
    public static class ModMetadataExtensions
    {
        /*
         *                 Resolvers[modMetadata].HookResolution();
                // TODO: null safety here
                string manifestFile = modMetadata.Assembly!.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(MANIFEST_NAME))!;
                using Stream stream = modMetadata.Assembly.GetManifestResourceStream(manifestFile)!;
                using TextReader reader = new StreamReader(stream);
                modMetadata.Manifest = JsonConvert.DeserializeObject<ModFileManifest>(reader.ReadToEnd());
                modMetadata.Mod = 
                Registrar.Register(new Identifier("engine", modMetadata.Manifest!.ModId), modMetadata);
         */

        public static void LoadManifestFile(this IModMetadata metadata) {
            // TODO: null safety here
            string manifestFile = metadata.Assembly!.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(AssemblyLoader.MANIFEST_NAME))!;
            using Stream stream = metadata.Assembly.GetManifestResourceStream(manifestFile)!;
            using TextReader reader = new StreamReader(stream);
            metadata.Manifest = JsonConvert.DeserializeObject<ModFileManifest>(reader.ReadToEnd());
        }

        public static void InstantiateMod(this IModMetadata metadata) {
            // TODO: null safety here
            Type[] types = metadata.Assembly!.GetTypes();

            foreach (Type type in types) {
                if (!typeof(IMod).IsAssignableFrom(type)) continue;
                if (type.GetConstructor(Array.Empty<Type>()) is null || type.IsAbstract || type.IsInterface) continue;
                
                metadata.Mod = (IMod) Activator.CreateInstance(type)!;
            }
        }
    }
}