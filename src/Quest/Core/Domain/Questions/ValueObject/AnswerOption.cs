using System;
using System.Linq;
using System.Collections.Generic;

namespace Domain.Questions.ValueObject
{
    public class AnswerOption
    {
        public int Option { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public AnswerOption(int option, string code, string value, bool isCorrect)
        {
            Option = option;
            Code = code;
            Value = value;
            IsCorrect = isCorrect;
        }

        public static List<AnswerOption> CreateOptions(string correctAnswer, List<string> options)
        {
            var random = new Random();
            var correctAnswerOptionNumber = random.Next(0, options.Count);
            var answerOptions = new AnswerOption[options.Count + 1];
            answerOptions[correctAnswerOptionNumber] = new AnswerOption(correctAnswerOptionNumber + 1, correctAnswerOptionNumber.ToString(), correctAnswer, true);

            var currentOptionNumber = correctAnswerOptionNumber;
            foreach (var option in options)
            {
                var nextOptionNumber = (currentOptionNumber + 1) % (answerOptions.Length);
                answerOptions[nextOptionNumber] = new AnswerOption(nextOptionNumber + 1, nextOptionNumber.ToString(), option, false);
                currentOptionNumber = nextOptionNumber;
            }

            return answerOptions.ToList();
        }
    }
}