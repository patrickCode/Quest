using System;

namespace Common.Exceptions
{
    public class DomainValidationException: ApplicationException
    {
        public DomainValidationException(Guid correlationId, string message, int code, Exception innerException)
            :base(correlationId, message, code, innerException) { }
        public DomainValidationException(Guid correlationId, string message, int code)
            : base(correlationId, message, code) { }

        public string Suggestion { get; set; }
    }
}