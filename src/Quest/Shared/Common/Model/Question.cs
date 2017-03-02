using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Model
{
    public class Question: DocumentEntity
    {
        public Question() { }
        public string Value { get; set; }
        public int DifficultLevel { get; set; }
        public string QuestionTypeCode { get; set; }
        public string AnswerTypeCode { get; set; }
        public string CorrectAnswer { get; set; }
        public AnswerOptions Options { get; set; }
        public List<Category> Categories { get; set; }
        public string Tags { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifedOn { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            var otherQuestion = obj as Question;
            if (otherQuestion == null)
                return false;

            return (Id == otherQuestion.Id
                && Value == otherQuestion.Value
                && CorrectAnswer == otherQuestion.CorrectAnswer
                && AnswerTypeCode == otherQuestion.AnswerTypeCode
                && QuestionTypeCode == otherQuestion.QuestionTypeCode
                && Tags == otherQuestion.Tags
                && DifficultLevel == otherQuestion.DifficultLevel);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class AnswerOptions
    {
        public AnswerOptions() { }
        public int Option { get; set; }
        public string Code { get; set; }
        public string Answer { get; set; }
        public bool IsCorrect { get; set; }
    }
}