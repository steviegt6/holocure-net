using System.Reflection;
using HoloCure.EventBus.Attributes;
using HoloCure.EventBus.Exceptions;

namespace HoloCure.EventBus.Extensions
{
    public static class EventBusExtensions
    {
        /// <summary>
        ///     Dispatch an event to all registered listeners.
        /// </summary>
        /// <param name="eventBus">this</param>
        /// <param name="theEvent">The event instance to dispatch.</param>
        public static void DispatchEvent<TEvent>(this IEventBus eventBus, TEvent theEvent)
            where TEvent : IEvent {
            eventBus.DispatchEvent(typeof(TEvent), theEvent);
        }

        /// <summary>
        ///     Register a listener in some form for an event.
        /// </summary>
        /// <param name="eventBus">this</param>
        /// <param name="eventDelegate">The event delegate.</param>
        public static void RegisterDelegate<TEvent>(this IEventBus eventBus, Action<TEvent> eventDelegate)
            where TEvent : IEvent {
            eventBus.RegisterDelegate(typeof(TEvent), theEvent => eventDelegate((TEvent) theEvent));
        }

        private static void RegisterGenericTypedDelegate(Type type, IEventBus eventBus, Delegate d) {
            MethodInfo method = typeof(EventBusExtensions).GetMethod(nameof(RegisterDelegate), BindingFlags.Public | BindingFlags.Static)!;
            MethodInfo generic = method.MakeGenericMethod(type);
            generic.Invoke(null, new object[] {eventBus, d});
        }

        public static void RegisterStaticType(this IEventBus eventBus, Type type) {
            foreach ((Type eventType, MethodInfo listener) in GetMethods(type, BindingFlags.Public | BindingFlags.Static)) {
                Type actionType = typeof(Action<>).MakeGenericType(eventType);
                RegisterGenericTypedDelegate(eventType, eventBus, Delegate.CreateDelegate(actionType, null, listener));
            }
        }

        public static void RegisterInstance(this IEventBus eventBus, object instance) {
            foreach ((Type eventType, MethodInfo listener) in GetMethods(instance.GetType(), BindingFlags.Public | BindingFlags.Instance)) {
                Type actionType = typeof(Action<>).MakeGenericType(eventType);
                RegisterGenericTypedDelegate(eventType, eventBus,  Delegate.CreateDelegate(actionType, instance, listener));
            }
        }

        private static IEnumerable<(Type, MethodInfo)> GetMethods(Type type, BindingFlags flags) {
            // Collect all public static methods
            IEnumerable<MethodInfo> methods = type.GetMethods(flags);

            // Cull down to all that are decorated with the expected attribute.
            methods = methods.Where(x => x.GetCustomAttribute<SubscriberAttribute>() != null);

            foreach (MethodInfo method in methods) {
                ParameterInfo[] parameters = method.GetParameters();

                if (parameters.Length != 1) {
                    if (parameters.Length == 0) {
                        throw new NotEnoughParametersInEventListenerException(
                            $"Event listener method \"{method.DeclaringType?.Name ?? "<no type>"}::{method.Name}\" has no parameters."
                        );
                    }

                    throw new TooManyParametersInEventListenerException(
                        $"Event listener method \"{method.DeclaringType?.Name ?? "<no type>"}::{method.Name}\" has too many parameters."
                    );
                }

                Type parameterType = parameters.Single().ParameterType;

                if (!typeof(IEvent).IsAssignableFrom(parameterType))
                    throw new ParameterIsNotEventInEventListenerException(
                        $"Event listener method \"{method.DeclaringType?.Name ?? "<no type>"}::{method.Name}\" has one parameter but it does not implement {nameof(IEvent)}."
                    );

                yield return (parameterType, method);
            }
        }
    }
}