namespace Eventing
{
    public abstract class Subscription(Key key, IEventAggregator aggregator) : ISubscription
    {
        public Key Key { get; protected set; } = key;

        public bool Released { get; protected set; }

        public int Invocations { get; protected set; }

        protected IEventAggregator Aggregator { get; } = aggregator;

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

        public virtual void Invoke<T>(T data)
        {
            Increment();
            InvokeCore(data);
        }

        protected abstract void InvokeCore(object? data);

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
