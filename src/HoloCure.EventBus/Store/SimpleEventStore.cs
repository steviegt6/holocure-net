using System.Runtime.CompilerServices;

namespace HoloCure.EventBus.Store
{
    /// <summary>
    ///     A simple <see cref="IEventStore"/> implementation.
    /// </summary>
    public class SimpleEventStore : IEventStore
    {
        protected class DefaultDictionary : Dictionary<Type, HashSet<IEventSubscriber>>
        {
            public new HashSet<IEventSubscriber> this[Type key]
            {
                get {
                    if (TryGetValue(key, out HashSet<IEventSubscriber>? value)) return value;
                    return this[key] = new HashSet<IEventSubscriber>();
                }

                set => base[key] = value;
            }
        }
        
        protected readonly DefaultDictionary Subscribers = new();
        
        public virtual IEventSubscriber RegisterSubscriber(Type eventType, IEventSubscriber subscriber) {
            HashSet<IEventSubscriber> subscribers = Subscribers[eventType];
            subscriber.OnRegistered(this);
            subscribers.Add(subscriber);
            return subscriber;
        }

        public virtual void UnregisterSubscriber(Type eventType, IEventSubscriber subscriber) {
            HashSet<IEventSubscriber> subscribers = Subscribers[eventType];
            subscriber.OnUnregistered(this);
            subscribers.Remove(subscriber);
        }

        // Other event stores will probably want to override this, mostly.
        public virtual IEnumerable<IEventSubscriber> GetSubscribers(Type eventType) {
            return Subscribers[eventType];
        }
    }
}