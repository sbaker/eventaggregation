using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EventAggregation
{
    public interface IEventAggregator
    {
        ISubscription Subscribe<T>(string name, Action<T> callback);

        bool Unsubscribe(ISubscription subscription);
    }

    public class EventAggregator : IEventAggregator
    {
        private readonly ConcurrentDictionary<string, IList<ISubscription>> _subscriptions = new ConcurrentDictionary<string, IList<ISubscription>>();

        public virtual ISubscription Subscribe<T>(string name, Action<T> callback)
        {
            throw new NotImplementedException();
        }

        public virtual bool Unsubscribe(ISubscription subscription)
        {
            throw new NotImplementedException();
        }
    }
}