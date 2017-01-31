using System;

namespace EventAggregation
{
    public interface ISubscription : IDisposable
    {
        int Invocations { get; }

        Key Key { get; }

        bool Unsubscribe();

        void Raise<T>(T data);
    }
}