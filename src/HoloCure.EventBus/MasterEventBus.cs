using HoloCure.EventBus.Store;

namespace HoloCure.EventBus
{
    /// <summary>
    ///     An <see cref="IEventBus"/> instance capable of dispatching to other <see cref="IEventBus"/> instance.
    /// </summary>
    public class MasterEventBus : IEventBus
    {
        /// <summary>
        ///     An unimplemented <see cref="IEventStore"/>, since the <see cref="MasterEventBus"/> managers child <see cref="IEventBus"/> instances that have their own <see cref="IEventStore"/> instances.
        /// </summary>
        public class MasterEventStore : IEventStore
        {
            public IEventSubscriber RegisterSubscriber(Type eventType, IEventSubscriber subscriber) {
                throw new NotImplementedException();
            }

            public void UnregisterSubscriber(Type eventType, IEventSubscriber subscriberId) {
                throw new NotImplementedException();
            }

            public IEnumerable<IEventSubscriber> GetSubscribers(Type eventType) {
                throw new NotImplementedException();
            }
        }

        protected virtual List<IEventBus> ChildEventBuses { get; } = new();

        public virtual IEventStore EventStore { get; } = new MasterEventStore();

        public virtual void Post(Type eventType, IEvent theEvent) {
            foreach (IEventBus childEventBus in ChildEventBuses) childEventBus.Post(eventType, theEvent);
        }

        public virtual void Subscribe(Type eventType, IEventSubscriber subscriber) {
            foreach (IEventBus eventBus in ChildEventBuses) eventBus.Subscribe(eventType, subscriber);
        }

        public virtual void AddEventBus(IEventBus eventBus) {
            ChildEventBuses.Add(eventBus);
        }

        public virtual void RemoveEventBus(IEventBus eventBus) {
            ChildEventBuses.Remove(eventBus);
        }
    }
}