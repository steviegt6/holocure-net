using HoloCure.EventBus.Store;

namespace HoloCure.EventBus
{
    /// <summary>
    ///     A simple event bus which supports dispatching events and registering listeners as delegates.
    /// </summary>
    public class SimpleEventBus : IEventBus
    {
        public virtual IEventStore EventStore { get; } = new SimpleEventStore();

        public virtual void Post(Type eventType, IEvent theEvent) {
            foreach (IEventSubscriber subscriber in EventStore.GetSubscribers(eventType))
                subscriber.Invoke(theEvent);
        }

        public virtual void Subscribe(Type eventType, IEventSubscriber subscriber) {
            EventStore.RegisterSubscriber(eventType, subscriber);
        }
    }
}