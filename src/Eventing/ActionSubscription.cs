using System;

namespace Eventing
{
  public class ActionSubscription<T>(Key key, Action<T> callback, IEventAggregator aggregator) : Subscription(key, aggregator)
  {
    private Action<T> Callback { get; } = callback;

    protected override void InvokeCore(object? value)
    {
      if (value is not T data)
      {
        throw new ArgumentException($"Invalid data type used to publish event expecting {typeof(T)}. Invalid data type: {value?.GetType()}");
      }

      Callback.Invoke(data);
    }
  }
}