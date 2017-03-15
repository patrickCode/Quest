using System;
using Common.Model;
using System.Linq;
using Common.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Services.QuestionServices.QueryServices
{
    public class QuestionsQueryService : IQuestionsQueryService
    {
        private readonly IDocumentReader<QuestionDto> _reader;

        public QuestionsQueryService(IDocumentReader<QuestionDto> reader)
        {
            _reader = reader;
        }

        public List<QuestionDto> Get()
        {
            return _reader.Get();
        }

        public List<QuestionDto> Get(Func<QuestionDto, bool> query)
        {
            return _reader.Query(query);
        }

        public QuestionDto Get(string id)
        {
            return _reader.Get(id);
        }

        public async Task<List<QuestionDto>> GetAsync()
        {
            return await _reader.GetAsync();
        }

        public async Task<List<QuestionDto>> GetAsync(Func<QuestionDto, bool> query)
        {
            return await _reader.QueryAsync(query);
        }

        public async Task<QuestionDto> GetAsync(string id)
        {
            return await _reader.GetAsync(id);
        }

        public QuestionDto GetQuestionByValue(string question)
        {
            return Get(ques => ques.Value.Equals(question)).FirstOrDefault();
        }

        public async Task<QuestionDto> GetQuestionByValueAsync(string question)
        {
            return (await GetAsync(ques => ques.Value.Equals(question))).FirstOrDefault();
        }

        public List<QuestionDto> GetByCategory(string category)
        {
            return Get(question => question.Categories.Any(ctg => ctg.Value.Equals(category)));
        }

        public async Task<List<QuestionDto>> GetByCategoryAsync(string category)
        {
            return await GetAsync(question => question.Categories.Any(ctg => ctg.Value.Equals(category)));
        }

        public List<QuestionDto> GetByUser(string userAlias)
        {
            return Get(question => question.CreatedBy.Equals(userAlias));
        }

        public async Task<List<QuestionDto>> GetByUserAsync(string userAlias)
        {
            return await GetAsync(question => question.CreatedBy.Equals(userAlias));
        }

        public List<QuestionDto> GetPublicQuestions()
        {
            return Get(question => question.IsPrivate == false);
        }

        public async Task<List<QuestionDto>> GetPublicQuestionsAsync()
        {
            return await GetAsync(question => question.IsPrivate == false);
        }
    }
}