using System;
using System.Linq;
using Common.Domain;
using System.Collections.Generic;
using Domain.Questions.ValueObject;
using Common.Exceptions;

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

        public Question(string id)
        {
            _id = id;
            _tags = new List<string>();
        }

        public Question()
        {   
            _tags = new List<string>();
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
        public Level Level
        {
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
                    : AnswerType.MCQ;
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

        private List<Category> _categories;
        public List<Category> Categories
        {
            get
            {
                return _categories;
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

        private Audit _audit;
        public Audit Audit
        {
            get
            {
                return _audit;
            }
        }

        private List<string> _tags;
        public string Tags
        {
            get
            {
                return string.Join(",", _tags);
            }
        }

        public void Save(string questionId, string user)
        {
            _id = questionId;
            _audit = Audit.EntityCreated(user);
        }

        public void SetQuestion(string question, QuestionType questionType, Guid trackingGuid = default(Guid), string mediaLink = null)
        {
            if (string.IsNullOrEmpty(question) || string.IsNullOrWhiteSpace(question))
                throw new DomainValidationException(trackingGuid, "Question cannot be null or empty", 4);

            _value = question;
            _questionType = questionType;
            if (_questionType == QuestionType.Text)
                return;
            if (string.IsNullOrEmpty(mediaLink))
                throw new DomainValidationException(trackingGuid, "Media Link must be provided for questions of audio/video or image type", 2);

            var mediaLinkValidity = Uri.TryCreate(mediaLink, UriKind.Absolute, out _mediaClipUri);
            if (!mediaLinkValidity)
                throw new DomainValidationException(trackingGuid, "The Media Link is not correctly formatted", 3);
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

        public void AddTags(params string[] tags)
        {
            if (_tags == null)
                _tags = new List<string>();
            _tags.AddRange(tags.ToList());
        }
    }
}