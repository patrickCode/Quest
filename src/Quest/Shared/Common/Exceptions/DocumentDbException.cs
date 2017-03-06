using System;

namespace Common.Exceptions
{
    public class DocumentDbException: ApplicationException
    {
        public DocumentDbException(Guid correlationId, string message, int code, Exception innerException)
            : base(correlationId, message, code, innerException)
        {
        }

        public DocumentDbException(Guid correlationId, string message, int code)
            : base(correlationId, message, code)
        {
        }
    }
}