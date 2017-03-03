using System.Threading.Tasks;

namespace Common.Domain
{
    public abstract class CommandProcessor
    {   
    }

    public abstract class CommandProcessor<C>: CommandProcessor where C: Command
    {
        public abstract CommandResult Process(C command);
        public abstract Task<CommandResult> ProcessAsync(C command);
    }
}