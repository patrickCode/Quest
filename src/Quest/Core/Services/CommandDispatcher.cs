using Common.Services;
using System;
using System.Linq;
using Common.Domain;
using System.Reflection;
using Common.Exceptions;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                .GetBaseClasesAndInterfaces()
                .Where(iface => iface.IsConstructedGenericType && iface.GetGenericArguments().First().GetTypeInfo().BaseType == genericProcessor.GetGenericArguments().First().GetTypeInfo().BaseType)
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
            CommandResult commandResult = ((dynamic)processor).Process(command as dynamic);
            return commandResult;
        }

        public async Task<CommandResult> DispatchAsync(Command command)
        {
            if (!_processors.ContainsKey(command.GetType()))
                throw new ProcessingException(Guid.NewGuid(), "No matching processor found", 14);

            var processor = _processors[command.GetType()];
            CommandResult commandResult = await ((dynamic)processor).ProcessAsync(command as dynamic);
            return commandResult;
        }
    }
}