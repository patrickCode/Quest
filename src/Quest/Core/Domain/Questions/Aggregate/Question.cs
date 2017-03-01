using System;
using System.Linq;
using Common.Domain;
using System.Collections.Generic;
using Domain.Questions.ValueObject;

namespace Domain.Questions.Aggregate
{
    public class Question : AggregateRoot
    {
        private string _id;
        public override string Id
        {
            get
            {
                return _id;
            }
        }

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
        }

        private Uri _mediaClipUri;
        public Uri MediaClipUri
        {
            get
            {
                return _mediaClipUri;
            }
        }

        private Level _level;
        public Level Level {
            get
            {
                return _level;
            }
        }

        private QuestionType _questionType;
        public QuestionType QuestionType
        {
            get
            {
                return _questionType;
            }
        }
        
        public AnswerType AnswerType
        {
            get
            {
                return (AnswerOptions == null || !AnswerOptions.Any()) 
                    ? AnswerType.Subjective 
                    : AnswerType.Subjective;
            }
        }

        private List<AnswerOption> _answerOptions;
        public List<AnswerOption> AnswerOptions
        {
            get
            {
                return _answerOptions;
            }
        }

        private Answer _answer;
        public Answer Answer
        {
            get
            {
                return _answer;
            }
        }

        public void Save(string questionId)
        {
            _id = questionId;
        }

        public void SetQuestion(string question, QuestionType questionType, string mediaLink = null)
        {
            _value = question;
            _questionType = questionType;
            if (_questionType == QuestionType.Text)
                return;
            if (string.IsNullOrEmpty(mediaLink))
                throw new Exception("Media Link must be provided for questions of audio/video or image type");

            var mediaLinkValidity = Uri.TryCreate(mediaLink, UriKind.Absolute, out _mediaClipUri);
            if (!mediaLinkValidity)
                throw new Exception("The Media Link is not correctly formatted");
        }

        public void SetMcqAnswers(string correctAnswer, List<string> options)
        {
            _answerOptions = AnswerOption.CreateOptions(correctAnswer, options);
            _answer = new Answer(_answerOptions.First(option => option.IsCorrect));
        }

        public void SetSubjectiveAnswer(string correctAnswer)
        {
            _answerOptions = null;
            _answer = new Answer(correctAnswer);
        }
    }
}