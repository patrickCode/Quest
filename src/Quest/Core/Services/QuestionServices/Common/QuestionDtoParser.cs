using System.Linq;
using Common.Model;
using Domain.Questions.Aggregate;
using System.Collections.Generic;

namespace Services.QuestionServices.Common
{
    public class QuestionDtoParser
    {
        public QuestionDto Create(Question question)
        {
            return new QuestionDto()
            {
                Id = question.Id,
                AnswerTypeCode = question.AnswerType.Code,
                Categories = GetCategories(question).ToList(),
                CorrectAnswer = question.Answer.Value,
                CreatedBy = question.Audit.CreatedBy,
                CreatedOn = question.Audit.CreatedOn,
                DifficultLevel = question.Level.Index,
                LastModifedOn = question.Audit.LastModifiedOn,
                LastModifiedBy = question.Audit.LastModifiedBy,
                MediaUrl = question.MediaClipUri != null ? question.MediaClipUri.ToString() : string.Empty,
                Options = question.AnswerOptions.Select(option => option.Value).ToList(),
                QuestionTypeCode = question.QuestionType.Code,
                Tags = question.Tags.Split(',').ToList(),
                Value = question.Value
            };
        }

        private IEnumerable<CategoryDto> GetCategories(Question question)
        {
            foreach(var category in question.Categories)
            {
                var categoryDto = new CategoryDto()
                {
                    Code = category.Code,
                    Value = category.Value
                };
                if (category.SubCategories == null || !category.SubCategories.Any())
                    yield return categoryDto;

                categoryDto.SubCatgories = new List<SubCategoryDto>();
                category.SubCategories.ForEach(subCategory =>
                {
                    categoryDto.SubCatgories.Add(new SubCategoryDto()
                    {
                        Code = subCategory.Code,
                        Value = subCategory.Value
                    });
                });
                yield return categoryDto;
            }
        }
    }
}