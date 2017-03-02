using System;

namespace Common.Exceptions
{
    public class ApplicationException: Exception
    {
        public Guid TrackingGuid { get; set; }
        public int Code { get; set; }

        public ApplicationException(Guid correlationId, string message, int code, Exception innerException)
            :base(message, innerException)
        {
            TrackingGuid = correlationId;
            Code = code;
        }

        public ApplicationException(Guid correlationId, string message, int code)
            : this(correlationId, message, code, null)
        {
        }
    }
}