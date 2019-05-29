using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Eventing.Tests
{
    public class ServiceCollectionTests
    {
        [Fact]
        public void AddEventingSingletonTest()
        {
            IServiceCollection services = new ServiceCollection()
                .AddEventing()
                .AddTransient<TestSubscriber>();

            var provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
            {
                var dependent1 = scope.ServiceProvider.GetRequiredService<TestSubscriber>();
                var dependent2 = scope.ServiceProvider.GetRequiredService<TestSubscriber>();
                var aggregator = scope.ServiceProvider.GetRequiredService<IEventAggregator>();
                aggregator.Publish(TestSubscriber.Key, "expected");

                Assert.Equal("expected", dependent1.Value);
                Assert.Equal(dependent1.Value, dependent2.Value);
                Assert.Same(dependent1.Aggregator, aggregator);
                Assert.Same(dependent2.Aggregator, aggregator);

                Assert.True(aggregator.Unsubscribe(dependent1.Subscription));
                Assert.True(aggregator.Unsubscribe(dependent2.Subscription));
                Assert.Equal(dependent1.Subscription.Invocations, dependent2.Subscription.Invocations);
            }
        }

        private class TestSubscriber
        {
            public TestSubscriber(IEventAggregator aggregator)
            {
                Aggregator = aggregator;
                Subscription = aggregator.Subscribe<string>(Key, SetValue);
            }

            private void SetValue(string s)
            {
                Value = s;
            }

            public static string Key { get; } = "test-key";

            public IEventAggregator Aggregator { get; }

            public ISubscription Subscription { get; }

            public string Value { get; set; }
        }
    }
}
