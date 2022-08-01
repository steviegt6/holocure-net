using HoloCure.EventBus.Store;

namespace HoloCure.EventBus
{
    /// <summary>
    ///     An event bus which handles dispatching.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        ///     The <see cref="IEventStore"/> which manages <see cref="IEventSubscriber"/>s that belong to this <see cref="IEventBus"/>.
        /// </summary>
        IEventStore EventStore { get; }

        /// <summary>
        ///     Post an event to all registered subscribers.
        /// </summary>
        /// <param name="eventType">The explicitly-defined event type.</param>
        /// <param name="theEvent">The event instance to dispatch.</param>
        void Post(Type eventType, IEvent theEvent);

        /// <summary>
        ///     Subscribes the given delegate to posts according to the given <paramref name="eventType"/>.
        /// </summary>
        /// <param name="eventType">The explicitly-defined event type.</param>
        /// <param name="subscriber">The subscriber that needs to be subscribed.</param>
        void Subscribe(Type eventType, IEventSubscriber subscriber);
    }
}