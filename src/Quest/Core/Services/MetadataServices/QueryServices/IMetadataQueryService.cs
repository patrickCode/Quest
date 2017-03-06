using Common.Model.Metadata;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Services.MetadataServices.QueryServices
{
    public interface IMetadataQueryService
    {
        Task<List<QuestionTypeDto>> GetQuestionTypesAsync();
        Task<List<AnswerTypeDto>> GetAnswerTypesAsync();
        Task<List<DifficultyLevelDto>> GetDefficultyLevelAsync();

        List<QuestionTypeDto> GetQuestionTypes();
        List<AnswerTypeDto> GetAnswerTypes();
        List<DifficultyLevelDto> GetDefficultyLevel();

    }
}