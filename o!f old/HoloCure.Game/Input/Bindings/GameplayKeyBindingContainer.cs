using System.Collections.Generic;
using osu.Framework.Input.Bindings;

namespace HoloCure.Game.Input.Bindings
{
    public class GameplayKeyBindingContainer : ConfigurableKeyBindingContainer<GameplayAction>
    {
        public override IEnumerable<IKeyBinding> DefaultKeyBindings =>
            new[]
            {
                new KeyBinding(InputKey.Z, GameplayAction.ConfirmStrafe),
                new KeyBinding(InputKey.X, GameplayAction.CancelSpecial),
                new KeyBinding(InputKey.Left, GameplayAction.Left),
                new KeyBinding(InputKey.Right, GameplayAction.Right),
                new KeyBinding(InputKey.Up, GameplayAction.Up),
                new KeyBinding(InputKey.Down, GameplayAction.Down)
            };

        public override Dictionary<GameplayAction, IKeyBinding> KeyBindingOverrides { get; } = new();
    }

    public enum GameplayAction
    {
        ConfirmStrafe,
        CancelSpecial,
        Left,
        Right,
        Up,
        Down
    }
}
