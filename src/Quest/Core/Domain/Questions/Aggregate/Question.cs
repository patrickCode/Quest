using System;
using System.Linq;
using Common.Domain;
using Common.Exceptions;
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

        public Question(string id)
        {
            _id = id;
            _tags = new List<string>();
            _categories = new List<Category>();
        }

        public Question()
        {   
            _tags = new List<string>();
            _categories = new List<Category>();
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

        public void Update(string questionId, string originalUser, DateTime createdOn, string currentUser)
        {
            _id = questionId;
            _audit = Audit.EntityModified(originalUser, createdOn, currentUser);
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

        public void SetMcqAnswers(string correctAnswer, List<string> options, Guid trackingGuid = default(Guid))
        {
            if (string.IsNullOrEmpty(correctAnswer) || string.IsNullOrWhiteSpace(correctAnswer))
                throw new DomainValidationException(trackingGuid, "Correct Answer Cannot be empty", 8);

            if (options == null || !options.Any())
                throw new DomainValidationException(trackingGuid, "No option present", 6);

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

        public void Categorize(string categoryName, string categoryCode, Guid trackingGuid = default(Guid))
        {
            if (string.IsNullOrEmpty(categoryName) || string.IsNullOrWhiteSpace(categoryName))
                throw new DomainValidationException(trackingGuid, "Category name cannot be empty", 11);

            if (string.IsNullOrEmpty(categoryCode) || string.IsNullOrWhiteSpace(categoryCode))
                throw new DomainValidationException(trackingGuid, "Category code cannot be empty", 11);

            var category = new Category(categoryName, categoryCode);
            _categories.Add(category);
        }

        public void SubCategorize(string categoryCode, string subCategoryName, string subCategoryCode, Guid trackingGuid = default(Guid))
        {
            var category = _categories.FirstOrDefault(cat => cat.Code.Equals(categoryCode));
            if (category == null)
                throw new DomainValidationException(trackingGuid, "No Category found to add sub-category", 10);

            if (string.IsNullOrEmpty(subCategoryName) || string.IsNullOrWhiteSpace(subCategoryName))
                throw new DomainValidationException(trackingGuid, "SubCategory name cannot be empty", 11);

            if (string.IsNullOrEmpty(subCategoryCode) || string.IsNullOrWhiteSpace(subCategoryCode))
                throw new DomainValidationException(trackingGuid, "SubCategory code cannot be empty", 11);

            category.SubCategories.Add(new SubCategory(subCategoryName, subCategoryCode));
        }

        public void SetDifficultyLevel(Level level)
        {
            _level = level;
        }
    }
}