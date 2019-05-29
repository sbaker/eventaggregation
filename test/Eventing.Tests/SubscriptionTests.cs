using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Eventing.Tests
{
    public class SubscriptionTests
    {
        [Fact]
        public void SubscribeToStringKeyTest()
        {
            string[] data = {"String1", "String2"};

            var aggregator = new EventAggregator();

            var sub = aggregator.Subscribe<IEnumerable<string>>("my-strings", list => Assert.All(list, s => Assert.Contains(s, data)));

            aggregator.Publish("my-strings", data);

            Assert.True(sub.Unsubscribe());
        }

        [Fact]
        public void SubscribeToThrowsWithInvalidDataStringKeyTest()
        {
            int[] invalidData = {1, 2};
            string[] data = { "String1", "String2" };

            var aggregator = new EventAggregator();

            var sub = aggregator.Subscribe<IEnumerable<string>>("my-strings", list => Assert.All(list, s => Assert.Contains(s, data)));

            Assert.Throws<ArgumentException>(() => aggregator.Publish("my-strings", invalidData));

            Assert.True(sub.Unsubscribe());
        }

        [Fact]
        public void SubscribeToIncrementsInvocationWhenRaisedStringKeyTest()
        {
            string[] data = { "String1", "String2" };

            var aggregator = new EventAggregator();

            var sub = aggregator.Subscribe<IEnumerable<string>>("my-strings", list => Assert.All(list, s => Assert.Contains(s, data)));

            aggregator.Publish("my-strings", data);

            Assert.Equal(1, sub.Invocations);

            Assert.True(sub.Unsubscribe());
        }

        [Fact]
        public void SubscribeToIntKeyTest()
        {
            string[] data = { "String1", "String2" };

            var aggregator = new EventAggregator();

            var sub = aggregator.Subscribe<IEnumerable<string>>(1, list => Assert.All(list, s => Assert.Contains(s, data)));

            aggregator.Publish(1, data);

            Assert.True(sub.Unsubscribe());
        }

        [Fact]
        public void MultipleSubscribeToIntKeyTest()
        {
            string[] data = { "String1", "String2" };

            var aggregator = new EventAggregator();

            var sub1 = aggregator.Subscribe<IEnumerable<string>>("asdf", list => {
                // Should never get here
                throw new Exception();
            });

            var sub2 = aggregator.Subscribe<IEnumerable<string>>(1, list => Assert.All(list, s => Assert.Contains(s, data)));

            aggregator.Publish(1, data);

            Assert.True(sub1.Unsubscribe());

            Assert.True(sub2.Unsubscribe());
        }

        [Fact]
        public void MultipleSubscribeToUnsubscribeStringKeyTest()
        {
            string[] data = { "String1", "String2" };

            var aggregator = new EventAggregator();

            var sub1 = aggregator.Subscribe<IEnumerable<string>>("asdf", list => {
                // Should never get here
                throw new Exception();
            });

            var sub2 = aggregator.Subscribe<IEnumerable<string>>(1, list => Assert.All(list, s => Assert.Contains(s, data)));

            Assert.True(sub1.Unsubscribe());

            // Should not throw an Exception here since we unsubscribed above.
            aggregator.Publish("asdf", data);

            aggregator.Publish(1, data);

            Assert.True(sub2.Unsubscribe());
        }

        [Fact]
        public void SubscribeToDisposeUnsubscribesStringAndIntKeyTest()
        {
            string[] data = { "String1", "String2" };

            var aggregator = new EventAggregator();

            ISubscription sub1;
            ISubscription sub2;

            using (sub1 = aggregator.Subscribe<IEnumerable<string>>("asdf", list => { throw new Exception(); }))
            {
                sub2 = aggregator.Subscribe<IEnumerable<string>>(1, list => Assert.All(list, s => Assert.Contains(s, data)));
            }

            // Should not throw an Exception here since we disposed of the subscription above.
            aggregator.Publish("asdf", data);

            Assert.True(((Subscription)sub1).Released);

            aggregator.Publish(1, data);

            sub2.Dispose();

            Assert.True(((Subscription)sub2).Released);
        }

        [Fact]
        public void CallbackHandlerToStringKeyTest()
        {
            //string[] data = { "String1", "String2" };

            //CallbackHandler<IEnumerable<string>> callback = context => Assert.All(context.Data, s => Assert.True(data.Contains(s)));

            //var aggregator = new EventAggregator();

            //var sub = aggregator.Subscribe("my-strings", callback);

            //aggregator.Raise("my-strings", data);

            //Assert.True(sub.Unsubscribe());
        }
    }
}
