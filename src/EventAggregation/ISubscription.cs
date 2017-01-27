using System;

namespace EventAggregation
{
    public interface ISubscription : IDisposable
    {
        Key Key { get; }

        bool Release();
    }
}