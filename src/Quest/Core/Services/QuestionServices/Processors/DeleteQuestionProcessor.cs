using Common.Model;
using Common.Domain;
using Common.Exceptions;
using Common.Interfaces;
using System.Threading.Tasks;
using Domain.Questions.Aggregate;
using Common.Domain.CommandResults;
using Services.QuestionServices.Commands;

namespace Services.QuestionServices.Processors
{
    public class DeleteQuestionProcessor : CommandProcessor<DeleteQuestion>
    {
        private readonly IDocumentReader<QuestionDto> _reader;
        private readonly IDocumentWriter<QuestionDto> _writer;

        public DeleteQuestionProcessor(IDocumentReader<QuestionDto> reader, IDocumentWriter<QuestionDto> writer)
        {
            _reader = reader;
            _writer = writer;
        }
            
        public override CommandResult Process(DeleteQuestion command)
        {
            return ProcessAsync(command).Result;
        }

        public async override Task<CommandResult> ProcessAsync(DeleteQuestion command)
        {
            var isQuestionPresent = await _reader.ExistsAsync(command.QuestionId);
            if (!isQuestionPresent)
                throw new DomainValidationException(command.Id, "Question doesn't exist", 15);

            var question = new Question();
            await _writer.DeleteAsync(command.QuestionId);
            return new VoidResult(command.Id, true);
        }
    }
}