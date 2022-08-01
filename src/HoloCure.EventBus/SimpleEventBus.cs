namespace HoloCure.EventBus
{
    /// <summary>
    ///     A simple event bus which supports dispatching events and registering listeners as delegates.
    /// </summary>
    public class SimpleEventBus : IEventBus
    {
        protected readonly Dictionary<Type, List<Action<IEvent>>> Listeners = new();

        public void DispatchEvent(Type eventType, IEvent theEvent) {
            foreach (Action<IEvent> listener in GetListeners(eventType))
                listener(theEvent);
        }

        public void RegisterDelegate(Type eventType, Action<IEvent> eventDelegate) {
            GetListeners(eventType).Add(eventDelegate);
        }

        protected List<Action<IEvent>> GetListeners(Type eventType) {
            if (Listeners.ContainsKey(eventType)) return Listeners[eventType];
            return Listeners[eventType] = new List<Action<IEvent>>();
        }
    }
}