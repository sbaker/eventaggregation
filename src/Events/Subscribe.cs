namespace Events
{
    public static class Subscribe
    {
        private static IEventAggregator Aggregator = new EventAggregator();

        public static void UseAggregator(IEventAggregator aggregator, bool disposeExisting = true)
        {
            var existing = Interlocked.Exchange(ref Aggregator, aggregator);

            if (disposeExisting)
            {
                existing.Dispose();
            }
        }

        public static ISubscription To<TData>(EventId eventId, Action<EventContext<TData>> callback)
        {
            return Aggregator.Subscribe(eventId, callback);
        }

        public static bool Unsubscribe(ISubscription subscription)
        {
            return Aggregator.Unsubscribe(subscription);
        }

        public static void Publish<T>(EventId eventId, T data)
        {
            Aggregator.Publish(eventId, data);
        }
    }
}