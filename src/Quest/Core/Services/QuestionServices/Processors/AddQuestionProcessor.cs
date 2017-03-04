using System;
using Common.Model;
using Common.Domain;
using Common.Interfaces;
using System.Threading.Tasks;
using Services.QuestionServices.Common;
using Services.QuestionServices.Commands;

namespace Services.QuestionServices.Processors
{
    public class AddQuestionProcessor : CommandProcessor<AddQuestion>
    {
        private readonly IDocumentReader<QuestionDto> _reader;
        private readonly IDocumentWriter<QuestionDto> _writer;

        private readonly DomainValidator _domainValidator;
        private readonly QuestionDtoParser _dtoParser;
        public AddQuestionProcessor(IDocumentReader<QuestionDto> reader, IDocumentWriter<QuestionDto> writer)
        {
            _reader = reader;
            _writer = writer;
            _dtoParser = new QuestionDtoParser();
            _domainValidator = new DomainValidator();
        }

        public override CommandResult Process(AddQuestion command)
        {
            var commandResult = ProcessAsync(command).Result;
            return commandResult;
        }

        public override async Task<CommandResult> ProcessAsync(AddQuestion command)
        {   
            var id = Guid.NewGuid().ToString();
            var question = _domainValidator.FromDto(command.NewQuestion, command.Id);

            question.Save(id, command.NewQuestion.CreatedBy);
            var document = _dtoParser.Create(question);

            await _writer.CreateOrUpdateAsync(document);

            return new IdentityResult(command.Id, id);
        }
    }
}