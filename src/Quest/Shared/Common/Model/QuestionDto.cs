using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Model
{
    public class QuestionDto: DocumentEntity
    {
        public QuestionDto() { }
        public string Value { get; set; }
        public int DifficultLevel { get; set; }
        public string QuestionTypeCode { get; set; }
        public string AnswerTypeCode { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> Options { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> Tags { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifedOn { get; set; }
        public string MediaUrl { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            var otherQuestion = obj as QuestionDto;
            if (otherQuestion == null)
                return false;

            return (Id == otherQuestion.Id
                && Value == otherQuestion.Value
                && CorrectAnswer == otherQuestion.CorrectAnswer
                && AnswerTypeCode == otherQuestion.AnswerTypeCode
                && QuestionTypeCode == otherQuestion.QuestionTypeCode
                && Tags.SequenceEqual(otherQuestion.Tags)
                && DifficultLevel == otherQuestion.DifficultLevel);
        }
    }

    public class AnswerOption
    {
        public AnswerOption() { }
        public int Option { get; set; }
        public string Code { get; set; }
        public string Answer { get; set; }
    }

    public class Category
    {
        public Category() { }
        public string Value { get; set; }
        public string Code { get; set; }
        public List<SubCategory> SubCategories { get; set; }

    }
    public class SubCategory
    {
        public SubCategory() { }
        public string Value { get; set; }
        public string Code { get; set; }
    }
}