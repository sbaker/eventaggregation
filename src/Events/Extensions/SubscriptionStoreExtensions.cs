using Events.Storage;

namespace Events
{
    public static class SubscriptionStoreExtensions
    {
        public static bool TryGet(this ISubscriptionStore store, EventId eventId, out IReadOnlyList<ISubscription> subscriptions)
        {
            subscriptions = store.Get(eventId);

            return subscriptions.Any();
        }
    }
}