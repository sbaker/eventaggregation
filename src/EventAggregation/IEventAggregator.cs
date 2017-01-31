using System;

namespace EventAggregation
{
    public interface IEventAggregator
    {
        ISubscription Subscribe<T>(Key key, Action<T> callback);

        bool Unsubscribe(ISubscription subscription);

        void Raise<T>(Key key, T data);
    }
}