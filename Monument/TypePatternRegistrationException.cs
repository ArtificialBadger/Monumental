using System;
using System.Runtime.Serialization;

namespace Monument
{
    [Serializable]
    internal class TypePatternRegistrationException : Exception
    {
        public TypePatternRegistrationException()
        {
        }

        public TypePatternRegistrationException(string message) : base(message)
        {
        }

        public TypePatternRegistrationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TypePatternRegistrationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}