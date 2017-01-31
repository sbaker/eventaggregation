using System;

namespace EventAggregation
{
    public static class Events
    {
        private static readonly IEventAggregator Aggregator = new EventAggregator();

        public static ISubscription Subscribe<T>(Key key, Action<T> callback)
        {
            return Aggregator.Subscribe(key, callback);
        }

        public static bool Unsubscribe(ISubscription subscription)
        {
            return Aggregator.Unsubscribe(subscription);
        }

        public static void Raise<T>(Key key, T data)
        {
            Aggregator.Raise(key, data);
        }
    }
}