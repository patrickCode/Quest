using Common.Model;
using Common.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Services.CategoryServices.QueryServices
{
    public interface ICategoriesQueryService: IQueryService<CategoryDto>
    {
        Task<CategoryDto> GetByCodeAsync(string code);
        CategoryDto GetByCode(string code);
        Task<List<SubCategoryDto>> GetSubCategoriesByCategoryCodeAsync(string categoryCode);
        List<SubCategoryDto> GetSubCategoriesByCategoryCode(string categoryCode);
    }
}