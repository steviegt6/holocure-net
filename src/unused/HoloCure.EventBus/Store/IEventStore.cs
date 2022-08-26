namespace HoloCure.EventBus.Store
{
    /// <summary>
    ///     Represents an object which may store and manage registered event subscribers.
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        ///     Registers an event subscriber.
        /// </summary>
        /// <param name="eventType">The event type to register this subscriber under.</param>
        /// <param name="subscriber">The subscriber to register.</param>
        /// <returns>The registered subscriber instance, which needs to be used when unregistering a subscriber.</returns>
        IEventSubscriber RegisterSubscriber(Type eventType, IEventSubscriber subscriber);
        
        /// <summary>
        ///     Unregisters an event subscriber.
        /// </summary>
        /// <param name="eventType">The event type that the subscriber was registered under.</param>
        /// <param name="subscriber">The registered subscriber to unregister.</param>
        void UnregisterSubscriber(Type eventType, IEventSubscriber subscriber);
        
        /// <summary>
        ///     Get a collection of subscribers which fall under this type.
        /// </summary>
        /// <param name="eventType">The event type to get subscribers for.</param>
        /// <returns>A collection of subscribers which fall under the given type.</returns>
        IEnumerable<IEventSubscriber> GetSubscribers(Type eventType);
    }
}