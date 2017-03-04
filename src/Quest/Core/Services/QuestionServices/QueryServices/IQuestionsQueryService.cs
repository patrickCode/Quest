using Common.Model;
using Common.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Services.QuestionServices.QueryServices
{
    public interface IQuestionsQueryService: IQueryService<QuestionDto>
    {
        List<QuestionDto> GetByCategory(string category);
        Task<List<QuestionDto>> GetByCategoryAsync(string category);
        List<QuestionDto> GetByUser(string userAlias);
        Task<List<QuestionDto>> GetByUserAsync(string userAlias);
    }
}