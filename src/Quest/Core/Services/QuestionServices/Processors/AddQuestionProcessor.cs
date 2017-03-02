using System;
using Common.Model;
using Common.Domain;
using Common.Interfaces;
using Common.Exceptions;
using Domain.Questions.ValueObject;
using Services.QuestionServices.Commands;
using Domain.Questions.Aggregate;

namespace Services.QuestionServices.Processors
{
    public class AddQuestionProcessor : CommandProcessor<AddQuestion>
    {
        private readonly IDocumentReader<QuestionDto> _reader;
        private readonly IDocumentWriter<QuestionDto> _writer;
        public AddQuestionProcessor(IDocumentReader<QuestionDto> reader, IDocumentWriter<QuestionDto> writer)
        {
            _reader = reader;
            _writer = writer;
        }
        public override CommandResult Process(AddQuestion command)
        {
            var questionDocument = command.NewQuestion;
            var id = Guid.NewGuid().ToString();

            var question = new Question();

            //Question Type
            var questionTypeCode = questionDocument.QuestionTypeCode;
            var questionType = QuestionType.Get(questionTypeCode);
            if (questionType == null)
                throw new DomainValidationException(command.Id, "Invalid Question Type Code", 1);
            question.SetQuestion(questionDocument.Value, questionType, command.Id, questionDocument.MediaUrl);

            //Answer Type
            var answerTypeCode = questionDocument.AnswerTypeCode;
            var answerType = AnswerType.Get(answerTypeCode);
            if (answerType == null)
                throw new DomainValidationException(command.Id, "Invalid Answer Type Code", 5);


            throw new NotImplementedException();
        }
    }
}