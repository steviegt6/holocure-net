namespace HoloCure.EventBus
{
    public class SimpleEventBus : IEventBus
    {
        protected readonly Dictionary<Type, List<Action<IEvent>>> Listeners = new();

        public void DispatchEvent(Type eventType, IEvent theEvent) {
            if (Listeners.TryGetValue(eventType, out List<Action<IEvent>>? listeners)) {
                foreach (Action<IEvent> listener in listeners) listener(theEvent);
            }
            else
                Listeners[eventType] = new List<Action<IEvent>>();
        }
        public void RegisterDelegate(Type eventType, Action<IEvent> eventDelegate) {
            (Listeners.ContainsKey(eventType) ? Listeners[eventType] : Listeners[eventType] = new List<Action<IEvent>>()).Add(eventDelegate);
        }
    }
}