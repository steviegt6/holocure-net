namespace HoloCure.EventBus.Store
{
    /// <summary>
    ///     An event subscriber, which is the object responsible for invoking subscribed methods/delegates.
    /// </summary>
    public interface IEventSubscriber
    {
        /// <summary>
        ///     Invokes the subscribed method/delegate.
        /// </summary>
        /// <param name="theEvent">The event the method/delegate was registered for.</param>
        void Invoke(IEvent theEvent);

        /// <summary>
        ///     Invoked when registered to an <see cref="IEventStore"/>.
        /// </summary>
        /// <param name="eventStore">The event store that this object was registered to.</param>
        void OnRegistered(IEventStore eventStore);

        
        /// <summary>
        ///     Invoked when unregistered from an <see cref="IEventStore"/>.
        /// </summary>
        /// <param name="eventStore">The event store that this object was unregistered from.</param>
        void OnUnregistered(IEventStore eventStore);
    }
}