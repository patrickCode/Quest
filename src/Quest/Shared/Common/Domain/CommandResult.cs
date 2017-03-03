using System;

namespace Common.Domain
{
    public class CommandResult
    {
        public CommandResult(Guid commandId)
        {
            CommandId = commandId;
        }
        public Guid CommandId { get; set; }
    }
}