namespace HoloCure.EventBus.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class SubscriberAttribute : Attribute
    {
    }
}