using System;

namespace Eventing
{
    public interface IEventAggregator
    {
        ISubscription Subscribe<T>(Key key, Action<T> callback);

        bool Unsubscribe(ISubscription subscription);

        void Publish<T>(Key key, T data);
    }
}