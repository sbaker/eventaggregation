namespace EventAggregation
{
    public abstract class Subscription : ISubscription
    {
        private bool _released;

        protected Subscription(Key key, IEventAggregator aggregator)
        {
            Key = key;
            Aggregator = aggregator;
        }

        public Key Key { get; protected set; }

        protected IEventAggregator Aggregator { get; }

        public bool Release()
        {
            if (Aggregator.Unsubscribe(this))
            {
                _released = true;
            }

            return _released;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_released)
            {
                Release();
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
