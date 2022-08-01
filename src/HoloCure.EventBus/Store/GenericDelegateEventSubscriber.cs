namespace HoloCure.EventBus.Store
{
    public class GenericDelegateEventSubscriber<TEvent> : DelegateEventSubscriber
        where TEvent : IEvent
    {
        public GenericDelegateEventSubscriber(Action<TEvent> action) : base((theEvent => action((TEvent) theEvent))) { }
    }
}