using System;

namespace Eventing
{
    public class EventSubscription<T> : Subscription
    {
        public EventSubscription(Key key, Action<T> callback, IEventAggregator aggregator) : base(key, aggregator)
        {
            Callback = callback;
        }

        private Action<T> Callback { get; }

        protected override void RaiseCore<TData>(TData data)
        {
            Callback.DynamicInvoke(data);
        }
    }
}