using System;

namespace Common.Exceptions
{
    public class ProcessingException : ApplicationException
    {
        public ProcessingException(Guid correlationId, string message, int code, Exception innerException)
            : base(correlationId, message, code, innerException)
        {
        }

        public ProcessingException(Guid correlationId, string message, int code)
            : base(correlationId, message, code)
        {
        }
    }
}