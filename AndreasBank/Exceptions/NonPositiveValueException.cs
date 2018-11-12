using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AndreasBank.Exceptions
{
    public class NonPositiveValueException : ApplicationException
    {
        public NonPositiveValueException()
        {
        }

        public NonPositiveValueException(string message) : base(message)
        {
        }

        public NonPositiveValueException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonPositiveValueException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
