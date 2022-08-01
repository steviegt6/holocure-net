namespace HoloCure.EventBus.Store
{
    public class DelegateEventSubscriber : IEventSubscriber
    {
        protected virtual Action<IEvent> ActionDelegate { get; }

        public DelegateEventSubscriber(Action<IEvent> action) {
            ActionDelegate = action;
        }

        public virtual void Invoke(IEvent theEvent) {
            ActionDelegate(theEvent);
        }

        public virtual void OnRegistered(IEventStore eventStore) { }

        public virtual void OnUnregistered(IEventStore eventStore) { }
    }
}