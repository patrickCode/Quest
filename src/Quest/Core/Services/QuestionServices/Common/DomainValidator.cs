using System;
using System.Linq;
using Common.Model;
using Common.Exceptions;
using Domain.Questions.Aggregate;
using Domain.Questions.ValueObject;

namespace Services.QuestionServices.Common
{
    public class DomainValidator
    {
        public Question FromDto(QuestionDto questionDocument, Guid trackingGuid)
        {   
            var question = new Question();

            //Set Question
            var questionTypeCode = questionDocument.QuestionTypeCode;
            var questionType = QuestionType.Get(questionTypeCode);
            if (questionType == null)
                throw new DomainValidationException(trackingGuid, "Invalid Question Type Code", 1);
            question.SetQuestion(questionDocument.Value, questionType, trackingGuid, questionDocument.MediaUrl);

            //Answer Type
            var answerTypeCode = questionDocument.AnswerTypeCode;
            var answerType = AnswerType.Get(answerTypeCode);
            if (answerType == null)
                throw new DomainValidationException(trackingGuid, "Invalid Answer Type Code", 5);

            if (answerType == AnswerType.MCQ)
            {
                if (questionDocument.Options == null || !questionDocument.Options.Any())
                    throw new DomainValidationException(trackingGuid, "No option present", 6);
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
                throw new DomainValidationException(trackingGuid, "Wrong Difficulty Level", 9);
            question.SetDifficultyLevel(difficultLevel);

            //TODO: Check if category code exisits
            //Categorize
            if (questionDocument.Categories == null || !questionDocument.Categories.Any())
                throw new DomainValidationException(trackingGuid, "Question must have at least 1 category", 12);

            questionDocument.Categories.ForEach(category =>
            {
                question.Categorize(category.Value, category.Code, trackingGuid);
                if (category.SubCatgories != null && category.SubCatgories.Any())
                {
                    category.SubCatgories.ForEach(subCategory =>
                    {
                        question.SubCategorize(category.Code, subCategory.Value, subCategory.Code, trackingGuid);
                    });
                }
            });

            return question;
        }
    }
}
