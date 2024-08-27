using Events.Storage.Internal;

namespace Events.Storage
{
    public abstract class SubscriptionStore : ISubscriptionStore
    {
        /// <summary>
        /// Returns the default <see cref="ConcurrentDictionarySubscriptionStore"/> implementation.
        /// </summary>
        public static readonly ISubscriptionStore Default = new ConcurrentDictionarySubscriptionStore();

        /// <inheritdoc />
        public abstract void Add(EventId eventId, ISubscription subscription);

        /// <inheritdoc />
        public abstract bool Remove(ISubscription subscription);

        /// <inheritdoc />
        public abstract IReadOnlyList<ISubscription> Get(EventId eventId);

        /// <inheritdoc />
        public abstract IEnumerable<ISubscription> GetAll();
    }
}