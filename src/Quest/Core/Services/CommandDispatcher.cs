using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Domain;
using System.Reflection;
using Common.Exceptions;

namespace Services
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly Dictionary<Type, CommandProcessor> _processors;
        public CommandDispatcher(IEnumerable<CommandProcessor> processors)
        {
            _processors = new Dictionary<Type, CommandProcessor>();

            var genericProcessor = typeof(CommandProcessor<>);

            foreach (var processor in processors)
            {
                var commandTypes = processor.GetType()
                .GetInterfaces()
                .Where(iface => iface == genericProcessor)
                .Select(iface => iface.GetGenericArguments().First())
                .ToList();

                if (_processors.Keys.Any(commandTypes.Contains))
                    throw new ProcessingException(Guid.NewGuid(), "Command cannot have multiple processor", 13);

                commandTypes.ForEach(type => _processors.Add(type, processor));
            }
        }

        public CommandResult Dispatch(Command command)
        {
            if (!_processors.ContainsKey(command.GetType()))
                throw new ProcessingException(Guid.NewGuid(), "No matching processor found", 14);

            var processor = _processors[command.GetType()];
            CommandResult commandResult = ((dynamic)processor).Process(command);
            return commandResult;
        }

        public async Task<CommandResult> DispatchAsync(Command command)
        {
            if (!_processors.ContainsKey(command.GetType()))
                throw new ProcessingException(Guid.NewGuid(), "No matching processor found", 14);

            var processor = _processors[command.GetType()];
            CommandResult commandResult = await ((dynamic)processor).ProcessAsync(command);
            return commandResult;
        }
    }
}