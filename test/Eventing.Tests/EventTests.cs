using Xunit;

namespace Eventing.Tests
{
    public class EventTests
    {
        [Fact]
        public void EventSubscriptionTest()
        {
            var subscription = Event.Subscribe<string>("key", s => Assert.True(!string.IsNullOrWhiteSpace(s) && s == "Event raised."));

            Event.Raise("key", "Event raised.");

            subscription.Unsubscribe();
        }
    }
}
