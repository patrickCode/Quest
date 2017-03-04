using System;

namespace Common.Domain
{
    public class IdentityResult: CommandResult
    {
        public IdentityResult(Guid commandId, string identity)
            :base(commandId)
        {
            Identity = identity;
        }
       public string Identity { get; set; }
    }
}