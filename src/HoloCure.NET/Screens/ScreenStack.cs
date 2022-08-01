using System.Collections.Generic;

namespace HoloCure.NET.Screens
{
    public class ScreenStack : IScreenStack
    {
        public IScreen? CurrentScreen { get; protected set; }
        
        public IScreen? PreviousScreen { get; protected set; }

        protected Stack<IScreen> Stack = new();

        public void EnterScreen(IScreen screen) {
            PreviousScreen = CurrentScreen;
        }

        public bool ExitScreen() {
            throw new System.NotImplementedException();
        }
    }
}