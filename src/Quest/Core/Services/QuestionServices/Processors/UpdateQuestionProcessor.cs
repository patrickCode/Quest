using Common.Model;
using Common.Domain;
using Common.Interfaces;
using Common.Exceptions;
using System.Threading.Tasks;
using Services.QuestionServices.Common;
using Services.QuestionServices.Commands;

namespace Services.QuestionServices.Processors
{
    public class UpdateQuestionProcessor: CommandProcessor<UpdateQuestion>
    {
        private readonly IDocumentReader<QuestionDto> _reader;
        private readonly IDocumentWriter<QuestionDto> _writer;

        private readonly DomainValidator _domainValidator;
        private readonly QuestionDtoParser _dtoParser;
        public UpdateQuestionProcessor(IDocumentReader<QuestionDto> reader, IDocumentWriter<QuestionDto> writer)
        {
            _reader = reader;
            _writer = writer;
            _dtoParser = new QuestionDtoParser();
            _domainValidator = new DomainValidator();
        }

        public override CommandResult Process(UpdateQuestion command)
        {
            var commandResult = ProcessAsync(command).Result;
            return commandResult;
        }

        public override async Task<CommandResult> ProcessAsync(UpdateQuestion command)
        {
            var originalQuestion = await _reader.GetAsync(command.UpdatedQuestion.Id);
            if (originalQuestion == null)
                throw new DomainValidationException(command.Id, "This question doesn't exist", 15)
                {
                    Suggestion = "Create this question as a new question"
                };

            var question = _domainValidator.FromDto(command.UpdatedQuestion, command.Id);

            question.Update(originalQuestion.Id, 
                originalQuestion.CreatedBy, 
                originalQuestion.CreatedOn, 
                command.UpdatedQuestion.LastModifiedBy);
            var document = _dtoParser.Create(question);

            await _writer.CreateOrUpdateAsync(document);

            return new IdentityResult(command.Id, originalQuestion.Id);
        }
    }
}