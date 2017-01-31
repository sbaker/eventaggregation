using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace EventAggregation
{
    public class EventAggregator : IEventAggregator
    {
        protected ConcurrentDictionary<Key, IList<ISubscription>> Subscriptions { get; } = new ConcurrentDictionary<Key, IList<ISubscription>>();

        public virtual ISubscription Subscribe<T>(Key key, Action<T> callback)
        {
            var subscription = new EventSubscription<T>(key, callback, this);

            Subscriptions.AddOrUpdate(key, new List<ISubscription> { subscription }, (k, list) =>
            {
                list.Add(subscription);
                return list;
            });

            return subscription;
        }

        public virtual bool Unsubscribe(ISubscription subscription)
        {
            if (Subscriptions.ContainsKey(subscription.Key))
            {
                return Subscriptions[subscription.Key].Remove(subscription);
            }

            return false;
        }

        public virtual void Raise<T>(Key key, T data)
        {
            if (Subscriptions.ContainsKey(key))
            {
                var list = Subscriptions[key];

                if (list.Count > 0)
                {
                    foreach (var subscription in list)
                    {
                        subscription.Raise(data);
                    }
                }
            }
        }
    }
}