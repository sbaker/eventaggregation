
using Eventing;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventing(this IServiceCollection services)
        {
            return services.AddEventing<EventAggregator>();
        }

        public static IServiceCollection AddEventing<TEventAggregator>(this IServiceCollection services) where TEventAggregator : class, IEventAggregator
        {
            return services.AddSingleton<IEventAggregator, TEventAggregator>();
        }
    }
}
