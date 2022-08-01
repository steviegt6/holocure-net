namespace HoloCure.EventBus.Store
{
    /// <summary>
    ///     A simple <see cref="IEventStore"/> implementation.
    /// </summary>
    public class SimpleEventStore : IEventStore
    {
        public virtual IDictionary<Type, IDictionary<int, IEventSubscriber>> Subscribers { get; } = new Dictionary<Type, IDictionary<int, IEventSubscriber>>();

        protected virtual Dictionary<Type, int> TotalCountMap { get; } = new();

        public virtual int RegisterSubscriber(Type eventType, IEventSubscriber subscriber) {
            IDictionary<int, IEventSubscriber> subscribers = GetSubscribersFromMap(eventType);
            int subscriberId = IncrementTotalCount(eventType);
            subscriber.OnRegistered(this);
            subscribers.Add(subscriberId, subscriber);
            return subscriberId;
        }

        public virtual void UnregisterSubscriber(Type eventType, int subscriberId) {
            IDictionary<int, IEventSubscriber> subscribers = GetSubscribersFromMap(eventType);
            subscribers[subscriberId]?.OnUnregistered(this);
            subscribers.Remove(subscriberId);
        }

        // Other event stores will probably want to override this, mostly.
        public virtual IEnumerable<IEventSubscriber> GetSubscribers(Type eventType) {
            return GetSubscribersFromMap(eventType).Values;
        }
        
        protected virtual int IncrementTotalCount(Type eventType) {
            int count = TotalCountMap.TryGetValue(eventType, out int currentCount) ? currentCount : 0;
            TotalCountMap[eventType] = count + 1;
            return count;
        }

        protected virtual IDictionary<int, IEventSubscriber> GetSubscribersFromMap(Type eventType) {
            return Subscribers.ContainsKey(eventType) ? Subscribers[eventType] : Subscribers[eventType] = new Dictionary<int, IEventSubscriber>();
        }
    }
}