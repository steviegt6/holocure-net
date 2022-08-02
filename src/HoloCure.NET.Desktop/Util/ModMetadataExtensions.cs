using System;
using System.IO;
using System.Linq;
using HoloCure.Core;
using HoloCure.Core.Util;
using HoloCure.Loader;
using HoloCure.NET.Desktop.Exceptions;
using HoloCure.NET.Desktop.Loader;
using Newtonsoft.Json;

namespace HoloCure.NET.Desktop.Util
{
    public static class ModMetadataExtensions
    {
        public static void LoadManifestFile(this IModMetadata metadata) {
            if (metadata.Assembly is null) {
                throw new ModLoadMissingAssemblyException(
                    metadata.GetModName(),
                    "Mod metadata passed without a loaded assembly when attempted to read a manifest!"
                );
            }

            string? manifestFile = metadata.Assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith(AssemblyLoader.MANIFEST_NAME));

            if (manifestFile is null) {
                throw new ModLoadMissingManifestException(
                    metadata.GetModName(),
                    "Attempted to load a mod without a manifest file! Does the assembly include a \"manifest.json\" file as an embedded resource?"
                );
            }

            using Stream? stream = metadata.Assembly.GetManifestResourceStream(manifestFile);

            if (stream is null) {
                throw new ModLoadMissingManifestException(
                    metadata.GetModName(),
                    "Attempted to load a manifest file that was either not present or not visible!"
                );
            }

            using TextReader reader = new StreamReader(stream);
            metadata.Manifest = JsonConvert.DeserializeObject<ModFileManifest>(reader.ReadToEnd());
        }

        public static void InstantiateMod(this IModMetadata metadata, IGameLauncher launcher) {
            if (metadata.Assembly is null) {
                throw new ModLoadMissingAssemblyException(
                    metadata.GetModName(),
                    "Mod metadata passed without a loaded assembly when attempted to instantiate the mod!"
                );
            }

            Type[] types = metadata.Assembly.GetTypes();

            foreach (Type type in types) {
                if (!typeof(IMod).IsAssignableFrom(type)) continue;
                if (type.GetConstructor(Array.Empty<Type>()) is null || type.IsAbstract || type.IsInterface) continue;

                if (metadata.Mod is not null) {
                    throw new ModLoadMultipleModInterfacesException(
                        metadata.GetModName(),
                        $"Attempted to load a mod with multiple instantiatable classes that implement {nameof(IMod)}!"
                    );
                }

                metadata.Mod = (IMod) Activator.CreateInstance(type)!;
                metadata.Mod.Dependencies = launcher.Dependencies;
                launcher.GetMasterEventBus().AddEventBus(metadata.Mod.EventBus);
                metadata.Mod.Initialize();
            }

            if (metadata.Mod is null) {
                throw new ModLoadMissingModInterfaceException(
                    metadata.GetModName(),
                    $"Mod assembly does not contain an instantiatable class that implements the {nameof(IMod)} interface!"
                );
            }
        }

        public static string GetModName(this IModMetadata metadata) {
            return Path.GetFileNameWithoutExtension(metadata.AssemblyPath);
        }
    }
}