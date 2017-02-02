//using System;

//namespace Eventing
//{
//    public delegate void CallbackHandler<T>(CallbackContext<T> context);

//    public class CallbackContext<T>
//    {
//        public CallbackContext(object caller, T data)
//        {
//            Caller = caller;
//            Data = data;
//            UtcDate = DateTime.UtcNow;
//        }

//        public DateTime UtcDate { get; set; }

//        public object Caller { get; set; }

//        public T Data { get; set; }
//    }
//}
