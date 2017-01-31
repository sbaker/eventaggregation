using System;
using Xunit;

namespace Eventing.Tests
{
    public class EventTests
    {
        [Fact]
        public void EventSubscriptionTest()
        {
            var subscription = Event.Subscribe<string>("key", Console.WriteLine);

            Event.Raise("key", "Event raised.");

            subscription.Unsubscribe();
        }
    }
}
