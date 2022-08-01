namespace HoloCure.EventBus
{
    /// <summary>
    ///     An event bus which handles dispatching.
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        ///     Dispatch an event to all registered listeners.
        /// </summary>
        /// <param name="eventType">The explicitly-defined event type.</param>
        /// <param name="theEvent">The event instance to dispatch.</param>
        void DispatchEvent(Type eventType, IEvent theEvent);

        /// <summary>
        ///     Register a listener in some form for an event.
        /// </summary>
        /// <param name="eventType">The explicitly-defined event type.</param>
        /// <param name="eventDelegate">The event delegate.</param>
        void RegisterDelegate(Type eventType, Action<IEvent> eventDelegate);
    }
}