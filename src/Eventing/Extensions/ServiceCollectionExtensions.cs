using Eventing;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEvents(this IServiceCollection services)
        {
            return services.AddEvents<EventAggregator>();
        }

        public static IServiceCollection AddEvents<TEventAggregator>(this IServiceCollection services) where TEventAggregator : class, IEventAggregator
        {
            return services.AddSingleton<IEventAggregator, TEventAggregator>();
        }
    }
}
