using System;
using System.Linq;
using Common.Model;
using Common.Domain;
using Common.Interfaces;
using Common.Exceptions;
using System.Threading.Tasks;
using Domain.Questions.Aggregate;
using Domain.Questions.ValueObject;
using Services.QuestionServices.Common;
using Services.QuestionServices.Commands;

namespace Services.QuestionServices.Processors
{
    public class AddQuestionProcessor : CommandProcessor<AddQuestion>
    {
        private readonly IDocumentReader<QuestionDto> _reader;
        private readonly IDocumentWriter<QuestionDto> _writer;

        private readonly QuestionDtoParser _dtoParser;
        public AddQuestionProcessor(IDocumentReader<QuestionDto> reader, IDocumentWriter<QuestionDto> writer)
        {
            _reader = reader;
            _writer = writer;
            _dtoParser = new QuestionDtoParser();
        }

        public override CommandResult Process(AddQuestion command)
        {
            var commandResult = ProcessAsync(command).Result;
            return commandResult;
        }

        public override async Task<CommandResult> ProcessAsync(AddQuestion command)
        {   
            var id = Guid.NewGuid().ToString();
            var question = CreateDomain(command);

            question.Save(id, command.NewQuestion.CreatedBy);
            var document = _dtoParser.Create(question);

            await _writer.CreateAsync(document);

            return new IdentityResult(command.Id, id);
        }

        private Question CreateDomain(AddQuestion command)
        {
            var questionDocument = command.NewQuestion;
            var question = new Question();

            //Set Question
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

            if (answerType == AnswerType.MCQ)
            {
                if (questionDocument.Options == null || !questionDocument.Options.Any())
                    throw new DomainValidationException(command.Id, "No option present", 6);
                question.SetMcqAnswers(questionDocument.CorrectAnswer, questionDocument.Options);
            }
            else
            {
                question.SetSubjectiveAnswer(questionDocument.CorrectAnswer);
            }

            //Add Tags
            question.AddTags(questionDocument.Tags.ToArray());

            //Difficulty Level
            var difficultLevel = Level.Get(questionDocument.DifficultLevel);
            if (difficultLevel == null)
                throw new DomainValidationException(command.Id, "Wrong Difficulty Level", 9);
            question.SetDifficultyLevel(difficultLevel);

            //TODO: Check if category code exisits
            //Categorize
            if (questionDocument.Categories == null || !questionDocument.Categories.Any())
                throw new DomainValidationException(command.Id, "Question must have at least 1 category", 12);

            questionDocument.Categories.ForEach(category =>
            {
                question.Categorize(category.Value, category.Code, command.Id);
                if (category.SubCatgories != null && category.SubCatgories.Any())
                {
                    category.SubCatgories.ForEach(subCategory =>
                    {
                        question.SubCategorize(category.Code, subCategory.Value, subCategory.Code, command.Id);
                    });
                }
            });

            return question;
        }
    }
}