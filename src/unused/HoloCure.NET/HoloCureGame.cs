using Microsoft.Xna.Framework;

namespace HoloCure.NET
{
    public sealed class HoloCureGame : Game
    {
        public GraphicsDeviceManager? GraphicsDeviceManager {
            get => (GraphicsDeviceManager?) Services.GetService(typeof(GraphicsDeviceManager));

            set {
                if (GraphicsDeviceManager is not null) Services.RemoveService(typeof(GraphicsDeviceManager));
                Services.AddService(typeof(GraphicsDeviceManager), value);
            }
        }
        public HoloCureGame(string storagePath) {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            Window.AllowUserResizing = false;
            Window.Title = "HoloCure";
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            
            base.Draw(gameTime);
        }
    }
}