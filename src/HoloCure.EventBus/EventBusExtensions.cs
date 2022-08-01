using System.Reflection;
using HoloCure.EventBus.Attributes;
using HoloCure.EventBus.Exceptions;

namespace HoloCure.EventBus
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
            // TODO: Null safety / checks.
            MethodInfo? method = typeof(EventBusExtensions).GetMethod(nameof(RegisterDelegate), BindingFlags.Public | BindingFlags.Static);
            MethodInfo? generic = method?.MakeGenericMethod(type);
            generic?.Invoke(null, new object[] {eventBus, d});
        }

        /// <summary>
        ///     Register any methods subscribing to an event in a listener type.
        /// </summary>
        /// <param name="eventBus">this</param>
        /// <param name="type">The static type.</param>
        public static void RegisterStaticType(this IEventBus eventBus, Type type) {
            RegisterMethods(eventBus, type, null, BindingFlags.Public | BindingFlags.Static);
        }

        /// <summary>
        ///     Register any methods subscribing to an event in a listener instance.
        /// </summary>
        /// <param name="eventBus">this</param>
        /// <param name="instance">The object instance.</param>
        public static void RegisterInstance(this IEventBus eventBus, object instance) {
            RegisterMethods(eventBus, instance.GetType(), instance, BindingFlags.Public | BindingFlags.Instance);
        }

        private static void RegisterMethods(IEventBus eventBus, Type type, object? instance, BindingFlags flags) {
            foreach ((Type eventType, MethodInfo listener) in GetMethods(type, flags)) {
                Type actionType = typeof(Action<>).MakeGenericType(eventType);
                RegisterGenericTypedDelegate(eventType, eventBus,  Delegate.CreateDelegate(actionType, instance, listener));
            }
        }

        private static IEnumerable<(Type, MethodInfo)> GetMethods(Type type, BindingFlags flags) {
            IEnumerable<MethodInfo> methods = type.GetMethods(flags);
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