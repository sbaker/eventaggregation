namespace Eventing
{
    public abstract class Subscription : ISubscription
    {
        protected Subscription(Key key, IEventAggregator aggregator)
        {
            Key = key;
            Aggregator = aggregator;
        }

        public Key Key { get; protected set; }

        public bool Released { get; protected set; }

        public int Invocations { get; protected set; }

        protected IEventAggregator Aggregator { get; }

        public bool Unsubscribe()
        {
            if (Aggregator.Unsubscribe(this))
            {
                Released = true;
            }

            return Released;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Publish<T>(T data)
        {
            Increment();
            PublishCore(data);
        }

        protected abstract void PublishCore<T>(T data);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !Released)
            {
                Unsubscribe();
            }
        }

        protected void Increment()
        {
            Invocations++;
        }
    }
}
