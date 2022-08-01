namespace HoloCure.EventBus.Store
{
    /// <summary>
    ///     Represents an object which may store and manage registered event subscribers.
    /// </summary>
    public interface IEventStore
    {
        /// <summary>
        ///     A map of all registered subscribers, where <see cref="Type"/>s point to collections of registered subscribers.
        /// </summary>
        /// <remarks>
        ///     This map should typically not be accessed or modified outside of the <see cref="IEventStore"/>.
        /// </remarks>
        IDictionary<Type, IDictionary<int, IEventSubscriber>> Subscribers { get; }
        
        /// <summary>
        ///     Registers an event subscriber.
        /// </summary>
        /// <param name="eventType">The event type to register this subscriber under.</param>
        /// <param name="subscriber">The subscriber to register.</param>
        /// <returns>A numerical identifier used to unregister this subscriber.</returns>
        int RegisterSubscriber(Type eventType, IEventSubscriber subscriber);
        
        /// <summary>
        ///     Unregisters an event subscriber.
        /// </summary>
        /// <param name="eventType">The event type that the subscriber was registered under.</param>
        /// <param name="subscriberId">The numerical identifier the subscriber was associated with.</param>
        void UnregisterSubscriber(Type eventType, int subscriberId);
        
        /// <summary>
        ///     Get a collection of subscribers which fall under this type.
        /// </summary>
        /// <param name="eventType">The event type to get subscribers for.</param>
        /// <returns>A collection of subscribers which fall under the given type.</returns>
        IEnumerable<IEventSubscriber> GetSubscribers(Type eventType);
    }
}