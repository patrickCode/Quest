using Common.Model.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services.MetadataServices.QueryServices;

namespace Web.API
{
    [Route("api")]
    public class MetadataController : Controller
    {
        private readonly IMetadataQueryService _queryService;
        public MetadataController(IMetadataQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet]
        [Route("questionTypes")]
        public async Task<List<QuestionTypeDto>> GetQuestionTypes()
        {
            return await _queryService.GetQuestionTypesAsync();
        }

        [HttpGet]
        [Route("answerTypes")]
        public async Task<List<AnswerTypeDto>> GetAnswerTypes()
        {
            return await _queryService.GetAnswerTypesAsync();
        }

        [HttpGet]
        [Route("difficultyLevels")]
        public async Task<List<DifficultyLevelDto>> GetDifficultLevels()
        {
            return await _queryService.GetDefficultyLevelAsync();
        }
    }
}