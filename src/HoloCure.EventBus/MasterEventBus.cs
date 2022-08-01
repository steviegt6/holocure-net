namespace HoloCure.EventBus
{
    /// <summary>
    ///     An <see cref="IEventBus"/> instance capable of dispatching to other <see cref="IEventBus"/> instance.
    /// </summary>
    public class MasterEventBus : IEventBus
    {
        protected virtual List<IEventBus> ChildEventBuses { get; } = new();

        public virtual void DispatchEvent(Type eventType, IEvent theEvent) {
            foreach (IEventBus childEventBus in ChildEventBuses) 
                childEventBus.DispatchEvent(eventType, theEvent);
        }

        public virtual void RegisterDelegate(Type eventType, Action<IEvent> eventDelegate) {
            foreach (IEventBus eventBus in ChildEventBuses) 
                eventBus.RegisterDelegate(eventType, eventDelegate);
        }

        public virtual void AddEventBus(IEventBus eventBus) {
            ChildEventBuses.Add(eventBus);
        }

        public virtual void RemoveEventBus(IEventBus eventBus) {
            ChildEventBuses.Remove(eventBus);
        }
    }
}