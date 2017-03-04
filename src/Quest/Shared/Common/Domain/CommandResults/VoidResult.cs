using System;

namespace Common.Domain.CommandResults
{
    public class VoidResult : CommandResult
    {
        public bool IsSuccess { get; set; }
        public VoidResult(Guid commandId, bool result) : base(commandId)
        {
            IsSuccess = result;
        }
    }
}