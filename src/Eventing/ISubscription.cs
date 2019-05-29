using System;

namespace Eventing
{
    public interface ISubscription : IDisposable
    {
        int Invocations { get; }

        Key Key { get; }

        bool Unsubscribe();

        void Publish<T>(T data);
    }
}