using Common.Domain;
using System.Threading.Tasks;

namespace Common.Services
{
    public interface ICommandDispatcher
    {
        CommandResult Dispatch(Command command);
        Task<CommandResult> DispatchAsync(Command command);
    }
}