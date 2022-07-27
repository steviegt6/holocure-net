using System;
using System.Collections.Generic;
using System.Linq;
using HoloCure.Game.Utils;
using osu.Framework.Input.Bindings;

namespace HoloCure.Game.Input
{
    public abstract class ConfigurableKeyBindingContainer<T> : KeyBindingContainer<T>
        where T : struct, Enum
    {
        public abstract Dictionary<T, IKeyBinding> KeyBindingOverrides { get; }

        protected override void ReloadMappings()
        {
            List<IKeyBinding> defaults = DefaultKeyBindings.ToList();

            for (int i = 0; i < defaults.Count; i++)
            {
                IKeyBinding binding = defaults[i];
                T? bindingEnum = binding.Action.AsEnum<T>();

                if (!bindingEnum.HasValue) continue;

                if (KeyBindingOverrides.ContainsKey(bindingEnum.Value)) defaults[i] = KeyBindingOverrides[bindingEnum.Value];
            }

            KeyBindings = defaults;
        }
    }
}
