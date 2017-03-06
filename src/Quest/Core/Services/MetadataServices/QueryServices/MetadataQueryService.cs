using Common.Interfaces;
using Common.Model.Metadata;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Services.MetadataServices.QueryServices
{
    public class MetadataQueryService : IMetadataQueryService
    {
        private readonly IDocumentReader<QuestionTypeDto> _questionTypeReader;
        private readonly IDocumentReader<AnswerTypeDto> _answerTypeReader;
        private readonly IDocumentReader<DifficultyLevelDto> _difficultLevelReader;

        public MetadataQueryService(IDocumentReader<QuestionTypeDto> questionTypeReader, IDocumentReader<AnswerTypeDto> answerTypeReader, IDocumentReader<DifficultyLevelDto> difficultLevelReader)
        {
            _questionTypeReader = questionTypeReader;
            _answerTypeReader = answerTypeReader;
            _difficultLevelReader = difficultLevelReader;
        }

        public async Task<List<QuestionTypeDto>> GetQuestionTypesAsync()
        {
            return await _questionTypeReader.QueryAsync(aType => aType.MetadataType == "QuestionType");
        }

        public async Task<List<AnswerTypeDto>> GetAnswerTypesAsync()
        {
            return await _answerTypeReader.QueryAsync(aType => aType.MetadataType == "AnswerType");
        }

        public async Task<List<DifficultyLevelDto>> GetDefficultyLevelAsync()
        {
            return await _difficultLevelReader.QueryAsync(aType => aType.MetadataType == "DifficultyLevel");
        }

        public List<QuestionTypeDto> GetQuestionTypes()
        {
            return _questionTypeReader.Query(aType => aType.MetadataType == "QuestionType");
        }

        public List<AnswerTypeDto> GetAnswerTypes()
        {
            return _answerTypeReader.Query(aType => aType.MetadataType == "AnswerType");
        }

        public List<DifficultyLevelDto> GetDefficultyLevel()
        {
            return _difficultLevelReader.Query(aType => aType.MetadataType == "DifficultyLevel");
        }
    }
}