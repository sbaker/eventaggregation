//namespace Eventing
//{
//    public class CallbackHandlerSubscription<T> : Subscription
//    {
//        public CallbackHandlerSubscription(Key key, CallbackHandler<T> callback, IEventAggregator aggregator) : base(key, aggregator)
//        {
//            Callback = callback;
//        }

//        private CallbackHandler<T> Callback { get; }

//        protected override void RaiseCore<TData>(TData data)
//        {
//            Callback.DynamicInvoke(new CallbackContext<TData>(Aggregator, data));
//        }
//    }
//}