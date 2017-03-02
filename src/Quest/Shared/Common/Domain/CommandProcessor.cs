namespace Common.Domain
{
    public abstract class CommandProcessor<C> where C: Command
    {
        public abstract CommandResult Process(C command);
    }
}