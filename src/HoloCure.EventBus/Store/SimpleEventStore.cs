using System.Runtime.CompilerServices;

namespace HoloCure.EventBus.Store
{
    /// <summary>
    ///     A simple <see cref="IEventStore"/> implementation.
    /// </summary>
    public class SimpleEventStore : IEventStore
    {
        protected readonly Dictionary<Type, HashSet<IEventSubscriber>> Subscribers = new();
        
        public virtual IEventSubscriber RegisterSubscriber(Type eventType, IEventSubscriber subscriber) {
            HashSet<IEventSubscriber> subscribers = GetSubscribersFromMap(eventType);
            subscriber.OnRegistered(this);
            subscribers.Add(subscriber);
            return subscriber;
        }

        public virtual void UnregisterSubscriber(Type eventType, IEventSubscriber subscriber) {
            HashSet<IEventSubscriber> subscribers = GetSubscribersFromMap(eventType);
            subscriber.OnUnregistered(this);
            subscribers.Remove(subscriber);
        }

        // Other event stores will probably want to override this, mostly.
        public virtual IEnumerable<IEventSubscriber> GetSubscribers(Type eventType) {
            return GetSubscribersFromMap(eventType);
        }

        protected virtual HashSet<IEventSubscriber> GetSubscribersFromMap(Type eventType) {
            return Subscribers.ContainsKey(eventType) ? Subscribers[eventType] : Subscribers[eventType] = new HashSet<IEventSubscriber>();
        }
    }
}