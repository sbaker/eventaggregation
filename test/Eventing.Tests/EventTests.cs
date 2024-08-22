using Xunit;

namespace Eventing.Tests
{
    public class EventTests
    {
        [Fact]
        public void EventSubscriptionTest()
        {
            var subscription = Events.Subscribe<string>("key", s => Assert.True(!string.IsNullOrWhiteSpace(s) && s == "Event raised."));

            Events.Publish("key", "Event raised.");

            subscription.Unsubscribe();
        }
    }
}
