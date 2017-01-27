using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EventAggregation
{
    public interface IEventAggregator
    {
        ISubscription Subscribe<T>(Key key, Action<T> callback);

        bool Unsubscribe(ISubscription subscription);
    }

    public class EventAggregator : IEventAggregator
    {
        private readonly ConcurrentDictionary<Key, IList<ISubscription>> _subscriptions = new ConcurrentDictionary<Key, IList<ISubscription>>();

        public virtual ISubscription Subscribe<T>(Key key, Action<T> callback)
        {
            //_subscriptions.AddOrUpdate(key, new[] {null});
            throw new NotImplementedException();
        }

        public virtual bool Unsubscribe(ISubscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}