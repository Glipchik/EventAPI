using System.Runtime.Serialization;

namespace EventAPI.Core.Exceptions
{
    [Serializable]
    public class EventDbSetNullException : Exception
    {
        public EventDbSetNullException() { }

        public EventDbSetNullException(string message) : base(message) { }

        public EventDbSetNullException(string message, Exception innerException) : base(message, innerException) { }
        
        protected EventDbSetNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
