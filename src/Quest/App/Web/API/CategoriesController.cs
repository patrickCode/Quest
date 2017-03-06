using Common.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services.CategoryServices.QueryServices;

namespace Web.API
{
    [Route("api/categories")]
    public class CategoriesController: Controller
    {
        private readonly ICategoriesQueryService _queryService;
        public CategoriesController(ICategoriesQueryService queryService)
        {
            _queryService = queryService;
        }

        [HttpGet]
        public async Task<List<CategoryDto>> Get()
        {
            return await _queryService.GetAsync();
        }

        [HttpGet]
        [Route("{code}")]
        public async Task<CategoryDto> Get(string code)
        {
            return await _queryService.GetByCodeAsync(code);
        }

        [HttpGet]
        [Route("{code}/subcategories")]
        public async Task<List<SubCategoryDto>> GetSubcategories(string code)
        {
            return await _queryService.GetSubCategoriesByCategoryCodeAsync(code);
        }
    }
}